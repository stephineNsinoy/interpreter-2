namespace interpreter.Functions.Operators.Expressions
{
    internal class Multiplicative
    {
        /******************************
            Multiplicative expressions
        *******************************/

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

            Console.WriteLine($"Cannot multiply values of types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return null;
        }

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

            Console.WriteLine($"Cannot divide values of types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return null;
        }

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

            Console.WriteLine($"Cannot get modulo of types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return null;
        }
    }
}
