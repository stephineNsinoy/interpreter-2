namespace interpreter.Functions.Operators.Expressions
{
    internal class Logical
    {
        /***********************************
            LOGICAL OPERATOR expressions
        ************************************/

        /// <summary>
        /// Uses Logical AND to compare two BOOLEAN values
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns>Boolean Value</returns>
        public static object? And(object? left, object? right)
        {
            if (left is bool l && right is bool r)
                return l && r;

            Console.WriteLine($"SEMANTIC ERROR: Cannot perform logical AND on types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return null;
        }

        /// <summary>
        /// Uses Logical OR to compare two BOOLEAN values
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns>Boolean Value</returns>
        public static object? Or(object? left, object? right)
        {
            if (left is bool l && right is bool r)
                return l || r;

            Console.WriteLine($"SEMANTIC ERROR: Cannot perform logical OR on types {FunctionsOp.GetObject(left)} and {FunctionsOp.GetObject(right)}");
            Environment.Exit(400);
            return null;
        }

        /// <summary>
        /// Converts a BOOLEAN value to its opposite value
        /// </summary>
        /// <param name="value">right side of the operator</param>
        /// <returns>Boolean Value</returns>
        public static object? Not(object? value)
        {
            if (value is bool b)
                return !b;

            Console.WriteLine($"SEMANTIC ERROR: Cannot perform logical NOT on type {FunctionsOp.GetObject(value?.GetType())}");
            return null;
        }
    }
}
