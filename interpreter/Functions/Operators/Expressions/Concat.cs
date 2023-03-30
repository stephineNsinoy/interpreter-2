namespace interpreter.Functions.Operators.Expressions
{
    internal class Concat
    {
        public static object? Concatenate(object? left, object? right)
        {
            if (left is bool l)
            {
                left = l.ToString().ToUpper();
            }

            if (right is bool r)
            {
                right = r.ToString().ToUpper();
            }

            return $"{left}{right}";
        }
    }
}
