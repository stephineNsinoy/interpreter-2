﻿using interpreter.Functions.Evaluators;

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
        /// Scans the input from the user and stores it in the dictionary
        /// </summary>
        /// <param name="varNameArray">Array of identifier</param>
        /// <param name="variableDict">Dictionary that stores the dataype and expression</param>
        /// <param name="varDeclarationDict">Dictionary that stores the dataype and identifier</param>
        public static object? Scan(string[] varNameArray, Dictionary<string, object?> variableDict, Dictionary<string, string> varDeclarationDict)
        {
            foreach (var variable in varNameArray)
            {
                SemanticErrorEvaluator.EvaluateIsVariableDefined(variable, variableDict);

                var userInput = Console.ReadLine();

                SemanticErrorEvaluator.EvaluateScanInput(userInput);

                var parsed = FunctionsOp.ValueParser(userInput!);

                variableDict[variable] = parsed;

                var dataType = varDeclarationDict[variable];

                SemanticErrorEvaluator.EvaluateDeclaration(dataType, varNameArray, parsed);
            }
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
        /// Parses the value to its corresponding data type
        /// </summary>
        /// <param name="value">value inputted by the user</param>
        /// <returns>Parsed value</returns>
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
            else if (value.Length > 1)
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

        /// <summary>
        /// Increment\Decrement a value if whether it is an identifier (with a value 
        /// or just a number)
        /// </summary>
        /// <param name="symbol">symbol whether ++ or --</param>
        /// <param name="value">the value to be incremented or decremented</param>
        /// <param name="isInt">if the value is int, throws an error otherwise</param>
        /// <returns>Incremented or Decremented value</returns>
        public static int? IncrementValue(string symbol, int value, bool isInt)
        {
            if (isInt)
            {
                if (symbol.Equals("++"))
                {
                    return value + 1;
                }
                else
                {
                    return value - 1;
                }
            }
            Console.WriteLine("SEMANTIC ERROR: Cannot increment or decrement a non-integer value!");
            Environment.Exit(400);
            return null;
        }
    }
}
