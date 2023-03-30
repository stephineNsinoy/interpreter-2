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

        /// <summary>
        /// Evaluatees the expression of the SWITCH CASE
        /// </summary>
        public static bool GetSwitchCaseBool(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l == r;

            if (left is float lf && right is float rf)
                return lf == rf;

            if (left is int lInt && right is float rFloat)
                return lInt == rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat == rInt;

            if (left is string lString && right is string rString)
                return lString == rString;

            if (left is char lChar && right is char rChar)
                return lChar == rChar;

            if (left is bool lBool && right is bool rBool)
                return lBool == rBool;

            return false;
        }

        /// <summary>
        /// Parses the inputted value in scan
        /// </summary>
        public static object ValueParser(string value)
        {
            if (int.TryParse(value, out int intValue))
            {
                return intValue;
            }
            else if (float.TryParse(value, out float floatValue))
            {
                return floatValue;
            }
            else if (value.Equals("TRUE", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (value.Equals("FALSE", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else if (value.Length == 1)
            {
                return value[0];
            }
            else if(value.Length > 1)
            {
                return value[0..^0];
            }
            else
            {
                Console.WriteLine("SEMANTIC ERROR: Unknown value type.");
                Environment.Exit(400);
                return null;
            }
        }
    }
}
