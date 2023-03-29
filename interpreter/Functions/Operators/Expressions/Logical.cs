namespace interpreter.Functions.Operators.Expressions
{
    internal class Logical
    {
        /***********************************
            LOGICAL OPERATOR expressions
        ************************************/

        public static object? And(object? left, object? right)
        {
            if (left is bool l && right is bool r)
                return l && r;

            Console.WriteLine($"Cannot perform logical AND on types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return null;
        }

        public static object? Or(object? left, object? right)
        {
            if (left is bool l && right is bool r)
                return l || r;

            Console.WriteLine($"Cannot perform logical OR on types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return null;
        }

        public static object? Not(object? value)
        {
            if (value is bool b)
                return !b;

            Console.WriteLine($"Cannot perform logical NOT on type {FunctionsOp.GetObject(value?.GetType())}");
            Environment.Exit(400);
            return null;
        }
    }
}
