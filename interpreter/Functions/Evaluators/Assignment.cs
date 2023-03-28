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
        public static void EvaluateAssignment(string[] varNameArray, object? value, string content)
        {
            if (value == null)
            {
                string variables = string.Join("=", varNameArray);
                Console.WriteLine($"SYNTAX ERROR: Incorrect statement: {variables}=?");
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
