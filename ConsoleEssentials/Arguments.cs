using System.Collections;
using System.Linq;

namespace ConsoleEssentials
{
    /// <summary>
    /// Used to handle arguments from a console in a consistent way.
    /// </summary>
    public static class Arguments
    {
        /// <summary>
        /// Parse all the argument strings to the interpreter to generate the options hashtable
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Options hashtable</returns>
        public static Hashtable GetOptions(string[] args)
        {
            Hashtable options = new Hashtable();
            for (int i = 0; i < args.Length; ++i)
            {
                string arg = args[i];
                if (!arg[0].Equals('-'))
                    continue;
                int j = i + 1;
                if (j >= args.Length || args[j][0].Equals('-'))
                    options.Add(arg.TrimStart('-').ToLower(), true);
                else if (i < args.Length)
                {
                    options.Add(arg.TrimStart('-').ToLower(), args[j]);
                    ++i;
                }
            }
            return options;
        }

        /// <summary>
        /// Verifies that the options required is indeed present in the options table.
        /// </summary>
        /// <param name="hashtable">The options hashtable containing the options.</param>
        /// <param name="requiredOptions">Array containing the names of all the required opions.</param>
        /// <returns>Array of all the missing required options.</returns>
        public static string[] CheckOptions(this Hashtable hashtable, string[] requiredOptions)
        {
            return requiredOptions.Where(requiredOption => !hashtable.ContainsKey(requiredOption.ToLower())).ToArray();
        }

        /// <summary>
        /// Gets the string value of the option.
        /// </summary>
        /// <param name="hashtable">The options hashtable containing the options.</param>
        /// <param name="name">Name of the option to get the string value of.</param>
        /// <returns>The string value of the named option.</returns>
        public static string GetOptionString(this Hashtable hashtable, string name)
        {
            object option = hashtable.GetOptionObject(name);
            return (string)option;
        }

        /// <summary>
        /// Gets the string value of the option if it is not null or empty, otherwise the original value is returned.
        /// </summary>
        /// <param name="hashtable"></param>
        /// <param name="originalValue"></param>
        /// <param name="name"></param>
        /// <returns>If the option does exists and has a value it gets returned, otherwise the original value is returned.</returns>
        public static string GetOptionStringIfNotNull(this Hashtable hashtable, string originalValue, string name)
        {
            string optionString = hashtable.GetOptionString(name);
            return !string.IsNullOrEmpty(optionString) ? optionString : originalValue;
        }

        /// <summary>
        /// Returns true if the option switch is present, false otherwise.
        /// </summary>
        /// <param name="hashtable">The options hashtable containing the options.</param>
        /// <param name="name">Name of the switch option.</param>
        /// <returns>True or false if the option name is present.</returns>
        public static bool GetOptionSwitch(this Hashtable hashtable, string name)
        {
            object option = hashtable.GetOptionObject(name);
            return option != null;
        }

        /// <summary>
        /// Checks if the option with a name is in the argument collection
        /// </summary>
        /// <param name="hashtable"></param>
        /// <param name="name"></param>
        /// <returns>True or false weather an option with the name exists in the options collection.</returns>
        public static bool ContainsOption(this Hashtable hashtable, string name)
        {
            return hashtable.ContainsKey(name.ToLowerInvariant());
        }

        private static object GetOptionObject(this Hashtable hashtable, string name)
        {
            return hashtable[name.ToLowerInvariant()];
        }
    }
}
