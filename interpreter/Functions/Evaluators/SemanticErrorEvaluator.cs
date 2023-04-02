using interpreter.Variables;

namespace interpreter.Functions.Evaluators
{
    public class SemanticErrorEvaluator
    {
        /************************************
             EVALUATION FOR DECLARATION
       *************************************/

        /// <summary>
        /// Evaluates if value conforms to data type
        /// </summary>
        private static bool EvaluateDataType(string dataType, object? value)
        {
            if (value == null)
            {
                return true; // Null can be assigned to any data type
            }

            return dataType switch
            {
                "INT" => value is int,
                "FLOAT" => value is float,
                "BOOL" => value is bool,
                "STRING" => value is string,
                "CHAR" => value is char,
                _ => false,// Invalid data type
            };
        }

        /// <summary>
        /// Evaluates declaration
        /// </summary>
        public static void EvaluateDeclaration(string dataType, string[] varNames, object? value)
        {
            bool isValid = true;

            foreach (var name in varNames)
            {
                // Checks if value assigned corresponds to data type
                if (!EvaluateDataType(dataType, value))
                {
                    Console.WriteLine($"SEMANTIC ERROR: Invalid value assigned to variable {name}");
                    isValid = false;
                }
            }
            if (!isValid)
            {
                Environment.Exit(400);
            }
        }

        /// <summary>
        /// Checks if variable is redeclared
        /// </summary>
        public static void EvaluateIsVariableReDeclared(List<VariableDeclaration> varDeclarations, Dictionary<string, object?> Variable)
        {
            var redeclaredVars1 = varDeclarations.GroupBy(x => x.Name).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            var redeclaredVars2 = varDeclarations.Select(x => x.Name).Where(x => Variable.ContainsKey(x!)).ToList();

            if (redeclaredVars1.Any() || redeclaredVars2.Any())
            {
                var redeclared = redeclaredVars1.Concat(redeclaredVars2).Distinct();
                Console.WriteLine($"SEMANTIC ERROR: Variable {string.Join(" - ", redeclared)} is already declared");
                Environment.Exit(400);
            }
        }


        /************************************
              EVALUATION FOR ASSIGNMENT
        *************************************/

        /// <summary>
        /// Checks if variable is not defined
        /// </summary>
        public static void EvaluateIsVariableDefined(string varName, Dictionary<string, object?> Variable)
        {
            if (!Variable.ContainsKey(varName))
            {
                Console.WriteLine($"SEMANTIC ERROR: {varName} is not defined");
                Environment.Exit(400);
            }
        }

        /******************************
           EVALUATOR FOR CONDITIONS
        *******************************/

        /// <summary>
        /// Checks if condition is Valid
        /// </summary>
        /// <param name="value">Boolean value</param>
        /// <returns>Value of the bool</returns>
        public static bool IsTrue(object? value)
        {
            if (value is bool b)
                return b;

            Console.WriteLine("SEMANTIC ERROR: Value is not boolean.");
            Environment.Exit(400);
            return false;
        }

        /// <summary>
        /// Checks if condition is Valid, and changes it to the opposite value
        /// </summary>
        /// <param name="value">Boolean value</param>
        /// <returns>The opposite value of the boolean</returns>
        public static bool IsFalse(object? value) => !IsTrue(value);


        /************************************
             EVALUATION FOR FUNCTIONS
       *************************************/

        /// <summary>
        /// Checks if function is not defined or if variable is not a function
        /// </summary>
        public static object? EvaluateIsFunctionDefined(string name, Dictionary<string, object?> Variable)
        {
            if (!Variable.ContainsKey(name))
            {
                Console.WriteLine($"SEMANTIC ERROR: Function {name} is not defined.");
                Environment.Exit(400);
            }

            if (Variable[name] is not Func<object?[], object?>)
            {
                Console.WriteLine($"SEMANTIC ERROR: {name} is not a function.");
                Environment.Exit(400);
            }
            return null;
        }

        /// <summary>
        /// Evaluates the scan input if it is a valid input
        /// </summary>
        /// <param name="input">Inputt of the user</param>
        /// <returns>Error if condition is met, nothing if its not met</returns>
        public static object? EvaluateScanInput(string? input)
        {
            if (input == null || input.Equals(""))
            {
                Console.WriteLine("SEMANTIC ERROR: Scan function expects input.");
                return null;
            }
            return null;
        }
    }
}
