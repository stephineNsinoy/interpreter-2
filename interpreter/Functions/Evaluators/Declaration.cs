using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using interpreter.Variables;

namespace interpreter.Functions.Evaluators
{
    internal class Declaration
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
        /// TODO:
        ///     Needs more testing for other cases
        /// </summary>
        public static void EvaluateDeclaration(string dataType, string[] varNames, object? value)
        {
            bool isValid = true;

            foreach (var name in varNames)
            {
                // Checks if only data type is specified no identifier
                if (name == "<missing IDENTIFIER>")
                {
                    Console.WriteLine("SYNTAX ERROR - MISSING: IDENTIFIER!");
                    Environment.Exit(400);
                }

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
        /// GOODS:
        ///     INT  y=, -> ERROR
        ///     INT test =  -> ERROR
        /// TODO:
        ///     this statement must be error:
        ///             INT hehe = 3
        ///             INT y= 2, z = 2, r= 
        ///             DISPLAY: hehe          
        /// </summary>
        public static void EvaluateDeclarationContent(List<VariableDeclaration> varDeclarations, List<IParseTree> content)
        {
            var secondValue = content[2].GetText();

            if (varDeclarations.Count == 0 || secondValue != varDeclarations[0].Name)
            {
                Console.WriteLine("Invalid declaration statement");
                Environment.Exit(400);
            }
        }

        /// <summary>
        /// Checks if there is no value in declaring a variable
        /// TODO:
        ///     Needs more testing for other cases
        /// </summary>
        public static void EvaluateIsValuePresent(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression) || expression == "," || expression == "=")
            {
                Console.WriteLine("SYNTAX ERROR: Value is missing");
                Environment.Exit(400);
            }
        }

        ///// <summary>
        ///// GOODS
        ///// Checks if variable is redeclared
        ///// </summary>
        public static void EvaluateIsVariableReDeclared(List<VariableDeclaration> varDeclarations, Dictionary<string, object?> Variable)
        {
            var redeclaredVars = varDeclarations.Select(x => x.Name).Where(x => Variable.ContainsKey(x!)).ToList();

            if (redeclaredVars.Any())
            {
                Console.WriteLine($"ERROR: Variable {string.Join(" - ", redeclaredVars)} is already declared");
                Environment.Exit(400);
            }
        }

        /// <summary>
        /// Checks if declaration is valid with expression already
        /// TODO:
        ///     Include other unknown characters
        ///     INT test=2, haha=haha -> ERROR
        ///     INT test=2, haha=test -> NOT ERROR
        ///     INT test =2, , haha=3 -> ERROR
        /// </summary>
        public static void EvaluateIsDeclarationValid(string content)
        {
            if (content == "=")
            {
                Console.WriteLine("SYNTAX ERROR: Invalid declaration statement");
                Environment.Exit(400);
            }
        }

        // IN-PROGRESS
        public static void EvaluateNotValidDeclaration<T>(T context) where T : ParserRuleContext
        {
            bool hasInt = context.GetText().Contains("INT");
            bool hasChar = context.GetText().Contains("CHAR");
            bool hasBool = context.GetText().Contains("BOOL");
            bool hasString = context.GetText().Contains("STRING");
            bool hasFloat = context.GetText().Contains("FLOAT");

            if (hasInt || hasBool || hasChar || hasString || hasFloat)
            {
                Console.WriteLine("SYNTAX ERROR: Declaration must only be placed after BEGIN CODE");
                Environment.Exit(400);
            }
        }
    }
}
