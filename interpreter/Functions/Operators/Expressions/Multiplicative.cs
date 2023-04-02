namespace interpreter.Functions.Operators.Expressions
{
    internal class Multiplicative
    {
        /******************************
            Multiplicative expressions
        *******************************/

        /// <summary>
        /// Multiplies two numbers (int or float)
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns>Product of the two numbers</returns>

        public static object? Multiply(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l * r;

            if (left is float lf && right is float rf)
                return lf * rf;

            if (left is int lInt && right is float rFloat)
                return lInt * rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat * rInt;

            Console.WriteLine($"SEMANTIC ERROR: Cannot multiply values of types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return null;
        }

        /// <summary>
        /// Divides two numbers (int or float)
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns>Quotient of the two numbers</returns>
        public static object? Divide(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l / r;

            if (left is float lf && right is float rf)
                return lf / rf;

            if (left is int lInt && right is float rFloat)
                return lInt / rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat / rInt;

            Console.WriteLine($"SEMANTIC ERROR: Cannot divide values of types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return null;
        }

        /// <summary>
        /// Divides two numbers (int or float)
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns>Remainder of the two numbers</returns>
        public static object? Modulo(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l % r;

            if (left is float lf && right is float rf)
                return lf % rf;

            if (left is int lInt && right is float rFloat)
                return lInt % rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat % rInt;

            Console.WriteLine($"SEMANTIC ERROR: Cannot get modulo of types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return null;
        }
    }
}
