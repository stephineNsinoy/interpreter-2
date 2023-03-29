using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        ///// <summary>
        ///// GOODS
        ///// Checks if variable is redeclared
        ///// </summary>
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

        public static bool IsTrue(object? value)
        {
            if (value is bool b)
                return b;

            Console.WriteLine("SEMANTIC ERROR: Value is not boolean.");
            Environment.Exit(400);
            return false;
        }

        public static bool IsFalse(object? value) => !IsTrue(value);


        /************************************
             EVALUATION FOR FUNCTIONS
       *************************************/

        /// <summary>
        /// Checks if function is not defined or if variable is not a function
        /// </summary>
        public static void EvaluateIsFunctionDefined(string name, Dictionary<string, object?> Variable)
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
        }
    }
}
