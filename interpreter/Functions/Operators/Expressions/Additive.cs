namespace interpreter.Functions.Operators.Expressions
{
    internal class Additive
    {
        /***************************
            Additive expressions
        ***************************/

        /// <summary>
        /// Adds two numbers (int or float)
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns>Sum of the two numbers</returns>
        public static object? Add(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l + r;

            if (left is float lf && right is float rf)
                return lf + rf;

            if (left is int lInt && right is float rFloat)
                return lInt + rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat + rInt;

            Console.WriteLine($"SEMANTIC ERROR: Cannot add values of types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return null;
        }

        /// <summary>
        /// Subtracts two numbers (int or float)
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns>Difference of the two numbers</returns>
        public static object? Subtract(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l - r;

            if (left is float lf && right is float rf)
                return lf - rf;

            if (left is int lInt && right is float rFloat)
                return lInt - rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat - rInt;

            Console.WriteLine($"SEMANTIC ERROR: Cannot subtract values of types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return null;
        }
    }
}
