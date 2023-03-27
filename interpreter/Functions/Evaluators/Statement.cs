namespace interpreter.Functions.Evaluators
{
    internal class Statement
    {
        /// <summary>
        /// IN-PROGRESS
        /// Evaluates if new line exists per line
        /// </summary>
        public static void EvaluateNewLine(string? newline)
        {
            if (newline == null)
            {
                Console.WriteLine("SYNTAX ERROR: Every line must contain only one statement");
                Environment.Exit(400);
            }
        }
    }
}
