using interpreter.Functions.Evaluators;

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
            var userInput = Console.ReadLine();

            // Split input on commas
            var inputValues = userInput!.Split(',');

            SemanticErrorEvaluator.EvaluateScanInput(varNameArray.Length, inputValues.Length);

            for (int i = 0; i < varNameArray.Length; i++)
            {
                var variable = varNameArray[i];
                var inputValue = inputValues[i].Trim();

                SemanticErrorEvaluator.EvaluateIsVariableDefined(variable, variableDict);

                var parsed = FunctionsOp.ValueParser(inputValue);

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
    }
}