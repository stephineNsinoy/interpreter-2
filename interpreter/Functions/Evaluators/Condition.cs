using interpreter.Grammar;

namespace interpreter.Functions.Evaluators
{
    internal class Condition
    {
        /******************************
           EVALUATOR FOR CONDITIONS
        *******************************/

        public static bool IsTrue(object? value)
        {
            if (value is bool b)
                return b;

            Console.WriteLine("ERROR: Value is not boolean.");
            Environment.Exit(400);
            return false;
        }

        public static bool IsFalse(object? value) => !IsTrue(value);

        /// <summary>
        /// Evaluates the else block if it is present
        /// </summary>
        public static bool? EvaluateElseBlock(CodeParser.ElseBlockContext context)
        {
            if (context == null)
            {
                Console.WriteLine("Error: No ELSE block found");
                Environment.Exit(1);
            }
            return null;
        }
    }
}
