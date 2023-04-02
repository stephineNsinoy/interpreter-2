namespace interpreter.Functions.Operators.Expressions
{
    internal class Comparative
    {
        /******************************
            Comparative expressions
        *******************************/

        /// <summary>
        /// Checks if two values are equal
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns>True or False</returns>
        public static bool IsEqual(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l == r;

            if (left is float lf && right is float rf)
                return lf == rf;

            if (left is int lInt && right is float rFloat)
                return lInt == rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat == rInt;

            if (left is string lString && right is string rString)
                return lString == rString;

            if (left is char lChar && right is char rChar)
                return lChar == rChar;

            if (left is bool lBool && right is bool rBool)
                return lBool == rBool;

            Console.WriteLine($"SEMANTIC ERROR: Cannot compare values of types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return false;
        }

        /// <summary>
        /// Checks if two values are not equal
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns>True or False</returns>
        public static bool IsNotEqual(object? left, object? right)
        {
            return !IsEqual(left, right);
        }

        /// <summary>
        /// Checks if left side of the operator is greater
        /// than the right side of the operatorv
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns>True or False</returns>
        public static bool GreaterThan(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l > r;

            if (left is float lf && right is float rf)
                return lf > rf;

            if (left is int lInt && right is float rFloat)
                return lInt > rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat > rInt;

            if (left is char lChar && right is char rChar)
                return lChar > rChar;

            Console.WriteLine($"SEMANTIC ERROR: Cannot compare values of types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return false;
        }

        /// <summary>
        /// Checks if right side of the operator is greater
        /// than the left side of the operatorv
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns>True or False</returns>
        public static bool LessThan(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l < r;

            if (left is float lf && right is float rf)
                return lf < rf;

            if (left is int lInt && right is float rFloat)
                return lInt < rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat < rInt;

            if (left is char lChar && right is char rChar)
                return lChar < rChar;

            Console.WriteLine($"SEMANTIC ERROR: Cannot compare values of types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return false;
        }

        /// <summary>
        /// Checks if left side of the operator is greater
        /// than or equal to the right side of the operatorv
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns>True or False</returns>
        public static bool GreaterThanOrEqual(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l >= r;

            if (left is float lf && right is float rf)
                return lf >= rf;

            if (left is int lInt && right is float rFloat)
                return lInt >= rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat >= rInt;

            if (left is char lChar && right is char rChar)
                return lChar >= rChar;

            Console.WriteLine($"SEMANTIC ERROR: Cannot compare values of types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return false;
        }

        /// <summary>
        /// Checks if right side of the operator is greater
        /// than or equal to the left side of the operatorv
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns>True or False</returns>
        public static bool LessThanOrEqual(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l <= r;

            if (left is float lf && right is float rf)
                return lf <= rf;

            if (left is int lInt && right is float rFloat)
                return lInt <= rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat <= rInt;

            if (left is char lChar && right is char rChar)
                return lChar >= rChar;

            Console.WriteLine($"SEMANTIC ERROR: Cannot compare values of types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return false;
        }
    }
}
