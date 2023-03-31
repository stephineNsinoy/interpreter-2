namespace interpreter.Functions.Operators.Expressions
{
    internal class Concat
    {
        /******************************
            Comparative expression
        *******************************/

        /// <summary>
        /// Concatenates two values
        /// </summary>
        /// <param name="left">left side of the operator</param>
        /// <param name="right">right side of the operator</param>
        /// <returns></returns>
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
