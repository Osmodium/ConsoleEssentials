using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleEssentials;

namespace Test
{
    class Program
    {
        // Required parameters
        private static string REQ_PARAM1 = "RequiredParam1";
        private static string REQ_PARAM2 = "RequiredParam2";

        // Optional parameters
        private static string OPTIONAL_PARAM = "OptionalParam";
        private static string OPTIONAL_SWITCH = "Switch";

        private static readonly string[] RequiredParameters = { REQ_PARAM1, REQ_PARAM2 };
        private static Hashtable m_Parameters;

        static void Main(string[] args)
        {
            m_Parameters = Arguments.GetOptions(args);
            string[] missingParameters = m_Parameters.CheckOptions(RequiredParameters);
            if (missingParameters != null && missingParameters.Length > 0)
            {
                // Linq aggregate used for demo, won't work on all versions on .Net
                Log.Error($"Missing these required options: {missingParameters.Aggregate((i, j) => $"{i}, {j}")}");
                return;
            }

            // Use "GetOptionString" if you know it's there.
            string requiredParam1Value = m_Parameters.GetOptionString(REQ_PARAM1);
            string requiredParam2Value = m_Parameters.GetOptionString(REQ_PARAM2);

            // Use "GetOptionStringIfNotNull" if its an optional string parameter. The first parameter of this method is the default value.
            string optionalParamValue = m_Parameters.GetOptionStringIfNotNull(null, OPTIONAL_PARAM);

            // Use "GetOptionSwitch" to set a bool to if the switch is set or not.
            bool optionalSwitchValue = m_Parameters.GetOptionSwitch(OPTIONAL_SWITCH);

            string mainParameter = m_Parameters.GetMainOption();

            if (!string.IsNullOrEmpty(mainParameter))
                Log.Error(mainParameter);

            Log.Information(requiredParam1Value);
            Log.Warning(requiredParam2Value);

            if (optionalSwitchValue)
            {
                if (!string.IsNullOrEmpty(optionalParamValue))
                    Log.Critical(optionalParamValue);
                else
                    Log.Information("No optional value supllied!");
            }

            if (Debugger.IsAttached)
                Console.ReadKey();
            
        }
    }
}
