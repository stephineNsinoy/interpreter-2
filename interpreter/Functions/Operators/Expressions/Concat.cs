namespace interpreter.Functions.Operators.Expressions
{
    internal class Concat
    {
        public static object? Concatenate(object? left, object? right)
        {
            return $"{left}{right}";

            //Console.WriteLine($"Cannot concatenate values of types {left?.GetType()} and {right?.GetType()}");
            //Environment.Exit(1);
            //return null;
        }
    }
}
