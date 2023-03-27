namespace interpreter.Functions.Evaluators
{
    internal class Function
    {
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
                Console.WriteLine($"ERROR: Function {name} is not defined.");
                Environment.Exit(1);
            }

            if (Variable[name] is not Func<object?[], object?>)
            {
                Console.WriteLine($"ERROR: {name} is not a function.");
                Environment.Exit(1);
            }
        }
    }
}
