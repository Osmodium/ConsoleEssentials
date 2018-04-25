# ConsoleEssentials

### Synopsis
This .NET library seeks to streamline the process of writing console applications. It's lightweight and facilitates handling of the most tedious tasks when creating a console application.

### Features

This is the list of current features

* Logging to console and/or file.
* Customizable log location and file format.
* Parse arguments.
* Verify arguments.
* Easy extraction of parameter values.

> More features will be added along the way as they are needed.

### Motivation
Through the course of a day at my work, I often come across some small task that requires automation or to create a simple POC of something new that can be isolated completely and tested separately. I had several "GenericConsole" projects lying around where stuff would be bunched together, or I would have 100 small projects that had 30% redundant code in it. This usually was all of the parameter parsing and logging in it, and every project would be a little bit different than the one before. So I finally decided to create a NuGet package for it, so that when ever I would update it, ALL of my previous projects would almost instantly benefit from that update.

### Installation
The easiest way to install it is to find it on the NuGet market place, but here's some alternatives.

**Package Manager**
> PM> Install-Package ConsoleEssentials -Version 1.1.0

**.NET CLI**
> dotnet add package ConsoleEssentials --version 1.1.0

**Paket CLI**
> paket add ConsoleEssentials --version 1.1.0

### Example Code

The example below showcases the parsing of parameters (arguments/args) and some simple loggin to the default location.

```csharp
using ConsoleEssentials;

// Required parameters
private static string REQ_PARAM1 = "RequiredParam1";
private static string REQ_PARAM2 = "RequiredParam2";

// Optional parameters
private static string OPTIONAL_PARAM = "OptionalParam";
private static string OPTIONAL_SWITCH = "Switch";

private static readonly string[] RequiredParameters = { REQ_PARAM1, REQ_PARAM2 };
private static Hashtable m_Parameters;

public static void Main(string[] args)
{
	// Parse the args to a Hashtable
	m_Parameters = Arguments.GetOptions(args);
	string[] missingParameters = m_Parameters.CheckOptions(RequiredParameters);
	if (missingParameters != null && missingParameters.Length > 0)
	{
		// Linq aggregate used for demo, won't work on all versions on .Net
		Log.Error($"Missing these required options: {missingParameters.Aggregate((i, j) => $"{i}, {j}")}");
		return;
	}
	
	// Use "GetOptionString" if you know it's there.
	string RequiredParam1Value = m_Parameters.GetOptionString(REQ_PARAM1);
	string RequiredParam2Value = m_Parameters.GetOptionString(REQ_PARAM2);

	// Use "GetOptionStringIfNotNull" if its an optional string parameter. The first parameter of this method is the default value.
	string OptionalParamValue = m_Parameters.GetOptionStringIfNotNull(null, OPTIONAL_PARAM);

	// Use "GetOptionSwitch" to set a bool to if the switch is set or not.
	bool OptionalSwitchValue = m_Parameters.GetOptionSwitch(OPTIONAL_SWITCH);

	Log.Information(RequiredParam1Value);
	Log.Warning(RequiredParam2Value);

	if (OptionalSwitchValue)
	{
		if (!string.IsNullOrEmpty(OptionalParamValue))
			Log.Critical(OptionalParamValue);
		else
			Log.Information("No optional value supllied!");
	}
	
	// Your code that utilizes the values
}
```

To run the console application we need to parse the arguments like these examples:

>Input

`Program.exe -RequiredParam1 "This is the first param value" -RequiredParam2 "C:\Temp files\lala" -Switch`

>Output

    25-Apr-18 15:33:15 - INFO  - This is the first param value
    25-Apr-18 15:33:15 - WARN  - C:\Temp files\lala
    25-Apr-18 15:33:15 - INFO  - No optional value supllied!


>Input

`Program.exe -RequiredParam1 "This is the first param value" -RequiredParam2 "C:\Temp files\lala" -OptionalParam "Here's the optional param" -Switch`

>Output

    25-Apr-18 15:34:35 - INFO  - This is the first param value
    25-Apr-18 15:34:35 - WARN  - C:\Temp files\lala
    25-Apr-18 15:34:35 - CRIT  - Here's the optional param

### Contribute
The easiest way to contribut is to make pull requests to the repository along with a description of what the commit contains.
Alternatively write an issue ticket [here](https://github.com/Osmodium/ConsoleEssentials/issues "Link").

### License
The MIT License ([MIT](https://github.com/Osmodium/ConsoleEssentials/blob/master/Nuget/Build/LICENSE.md "Link")) @ Christian Schubert
