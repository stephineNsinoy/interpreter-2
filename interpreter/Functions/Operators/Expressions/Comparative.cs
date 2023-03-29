namespace interpreter.Functions.Operators.Expressions
{
    internal class Comparative
    {
        /******************************
            Comparative expressions
        *******************************/
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

        public static bool IsNotEqual(object? left, object? right)
        {
            return !IsEqual(left, right);
        }

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
