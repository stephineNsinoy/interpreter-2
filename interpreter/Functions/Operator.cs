﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpreter.Functions
{
    public class Operator
    {
        public static object? Display(object?[] args)
        {
            var output = string.Join("", args.Select(arg => arg?.ToString()));
            output = output.Replace("True", "TRUE");
            output = output.Replace("False", "FALSE");
            Console.Write(output);
            return null;
        }

        private static object? GetObject(object? obj)
        {
            if (obj is int)
            {
                return "INT";
            }
            else if (obj is float)
            {
                return "FLOAT";
            }
            else if (obj is char)
            {
                return "CHAR";
            }
            else if (obj is bool)
            {
                return "BOOL";
            }
            else
            {
                return "NULL";
            }
        }

        /***************************
            Additive expressions
        ***************************/
        public static object? Add(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l + r;

            if (left is float lf && right is float rf)
                return lf + rf;

            if (left is int lInt && right is float rFloat)
                return lInt + rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat + rInt;

            Console.WriteLine($"Cannot add values of types {GetObject(left)} and {GetObject(right)}");
            Environment.Exit(1);
            return null;
        }

        public static object? Subtract(object? left, object? right)
        {
            if (left is int l && right is int r)
                return l - r;

            if (left is float lf && right is float rf)
                return lf - rf;

            if (left is int lInt && right is float rFloat)
                return lInt - rFloat;

            if (left is float lFloat && right is int rInt)
                return lFloat - rInt;

            Console.WriteLine($"Cannot subtract values of types {GetObject(left)} and {GetObject(right)}");
            Environment.Exit(1);
            return null;
        }

        public static object? Concatenate(object? left, object? right)
        {
            return $"{left}{right}";

            //Console.WriteLine($"Cannot concatenate values of types {left?.GetType()} and {right?.GetType()}");
            //Environment.Exit(1);
            //return null;
        }


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

            Console.WriteLine($"Cannot compare values of types {GetObject(left)} and {GetObject(right)}");
            Environment.Exit(1);
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

            Console.WriteLine($"Cannot compare values of types {GetObject(left)} and {GetObject(right)}");
            Environment.Exit(1);
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

            Console.WriteLine($"Cannot compare values of types {GetObject(left)} and {GetObject(right)}");
            Environment.Exit(1);
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

            Console.WriteLine($"Cannot compare values of types {GetObject(left)} and {GetObject(right)}");
            Environment.Exit(1);
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

            Console.WriteLine($"Cannot compare values of types {GetObject(left)} and {GetObject(right)}");
            Environment.Exit(1);
            return false;
        }


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

            Console.WriteLine($"Cannot multiply values of types {GetObject(left)} and {GetObject(right)}");
            Environment.Exit(1);
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

            Console.WriteLine($"Cannot divide values of types {GetObject(left)} and {GetObject(right)}");
            Environment.Exit(1);
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

            Console.WriteLine($"Cannot get modulo of types {GetObject(left)} and {GetObject(right)}");
            Environment.Exit(1);
            return null;
        }

        /***********************************
            LOGICAL OPERATOR expressions
        ************************************/

        public static object? And(object? left, object? right)
        {
            if (left is bool l && right is bool r)
                return l && r;

            Console.WriteLine($"Cannot perform logical AND on types {GetObject(left)} and {GetObject(right)}");
            Environment.Exit(1);
            return null;
        }

        public static object? Or(object? left, object? right)
        {
            if (left is bool l && right is bool r)
                return l || r;

            Console.WriteLine($"Cannot perform logical OR on types {GetObject(left)} and {GetObject(right)}");
            Environment.Exit(1);
            return null;
        }

        public static object? Not(object? value)
        {
            if (value is bool b)
                return !b;

            Console.WriteLine($"Cannot perform logical NOT on type {GetObject(value?.GetType())}");
            return null;
        }


       /******************************
              Unary expressions
       *******************************/
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
                Console.WriteLine($"Unary operator {symbol} cannot be applied to non-numeric value.");
                Environment.Exit(1);
            }

            return null;
        }


        /******************************
            WHILE loop expressions
        *******************************/

        public static bool IsTrue(object? value)
        {
            if (value is bool b)
                return b;

            Console.WriteLine("Value is not boolean.");
            Environment.Exit(1);
            return false;
        }

        public static bool IsFalse(object? value) => !IsTrue(value);
    }
}
