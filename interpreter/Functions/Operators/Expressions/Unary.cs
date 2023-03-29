﻿namespace interpreter.Functions.Operators.Expressions
{
    internal class Unary
    {
        /******************************
              Unary expressions
        ******************************/
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
