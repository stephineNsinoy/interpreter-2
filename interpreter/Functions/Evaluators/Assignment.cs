using interpreter.Grammar;

namespace interpreter.Functions.Evaluators
{
    internal class Assignment
    {
        /************************************
             EVALUATION FOR ASSIGNMENT
       *************************************/

        /// <summary>
        /// Checks if assignment is incomplete, no expression
        /// </summary>
        public static void EvaluateAssignment(string[] varNameArray, List<CodeParser.ExpressionContext?> values)
        {
            if (values.Count == 0)
            {
                string variables = string.Join("=", varNameArray);
                Console.WriteLine($"SYNTAX ERROR: Invaild statement: {variables}=?");
                Environment.Exit(400);
            }
            
            if (values.Count > 1)
            {
                Console.WriteLine($"SYNTAX ERROR - UNEXPECTED TOKEN: Invalid assignment statement");
                Environment.Exit(400);
            }
        }

        /// <summary>
        /// Checks if variable is not defined
        /// </summary>
        public static void EvaluateIsVariableDefined(string varName, Dictionary<string, object?> Variable)
        {
            if (!Variable.ContainsKey(varName))
            {
                Console.WriteLine($"ERROR: Variable {varName} is not defined");
                Environment.Exit(400);
            }
        }
    }
}
