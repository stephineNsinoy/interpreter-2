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
            Environment.Exit(1);
            return false;
        }

        public static bool IsFalse(object? value) => !IsTrue(value);
    }
}
