namespace interpreter.Functions.Operators.Expressions
{
    internal class Unary
    {
        /******************************
              Unary expressions
        ******************************/

        /// <summary>
        /// Changes the sign of a number
        /// </summary>
        /// <param name="symbol">Either '+' or '-'</param>
        /// <param name="expressionValue">Any number</param>
        /// <returns>New value of the number depending on the sign</returns>
        public static object? UnaryValue(string symbol, object? expressionValue)
        {
            if (expressionValue is int intValue)
            {
                if (symbol == "+")
                {
                    return intValue;
                }
                else if (symbol == "-")
                {
                    return -intValue;
                }
            }
            else if (expressionValue is float floatValue)
            {
                if (symbol == "+")
                {
                    return floatValue;
                }
                else if (symbol == "-")
                {
                    return -floatValue;
                }
            }
            else
            {
                Console.WriteLine($"SEMANTIC ERROR: Unary operator {symbol} cannot be applied to non-numeric value.");
                Environment.Exit(400);
            }

            return null;
        }
    }
}
