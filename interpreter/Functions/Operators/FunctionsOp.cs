using System.Text.RegularExpressions;

namespace interpreter.Functions.Operators
{
    internal class FunctionsOp
    {
        public static object? Display(object?[] args)
        {
            var output = string.Join("", args.Select(arg => arg?.ToString()));
            var trueRegex = new Regex(@"(?<![a-zA-Z])True(?![a-zA-Z])");
            output = trueRegex.Replace(output, "TRUE");
            var falseRegex = new Regex(@"(?<![a-zA-Z])False(?![a-zA-Z])");
            output = falseRegex.Replace(output, "FALSE");
            Console.Write(output);
            return null;
        }

        /// <summary>
        /// Gets the data type of the value
        /// </summary>
        public static object? GetObject(object? obj)
        {
            if (obj is int)
            {
                return "INT";
            }
            else if (obj is float)
            {
                return "FLOAT";
            }
            else if (obj is char)
            {
                return "CHAR";
            }
            else if (obj is bool)
            {
                return "BOOL";
            }
            else if (obj is string)
            {
                return "STRING";
            }
            else
            {
                return "NULL";
            }
        }

        /// <summary>
        /// Parses the string TRUE and FALSE to their corresponding 
        /// boolean values
        /// </summary>
        public static bool GetBool(string str)
        {
            return str.Equals("\"TRUE\"");
        }
    }
}
