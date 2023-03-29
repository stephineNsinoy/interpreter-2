using System.Text.RegularExpressions;

namespace interpreter.Functions.Operators
{
    internal class FunctionsOp
    {
        /// <summary>
        /// Prints the contents inside DISPLAY:
        /// </summary>
        public static object? Display(object? args)
        {
            if (args is bool b)
                args = b.ToString().ToUpper();

            Console.Write(args);
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
    }
}
