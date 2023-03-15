using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpreter.Functions
{
    public class Operator
    {
        private Evaluator evalute = new Evaluator();
        public static object? Display(object?[] args)
        {
            foreach (var arg in args)
            {
                Console.Write(arg);
            }

            return null;
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

            Console.WriteLine($"Cannot add values of types {left?.GetType()} and {right?.GetType()}");
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

            Console.WriteLine($"Cannot subtract values of types {left?.GetType()} and {right?.GetType()}");
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

            Console.WriteLine($"Cannot compare values of types {left?.GetType()} and {right?.GetType()}");
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

            Console.WriteLine($"Cannot compare values of types {left?.GetType()} and {right?.GetType()}");
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

            Console.WriteLine($"Cannot compare values of types {left?.GetType()} and {right?.GetType()}");
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

            Console.WriteLine($"Cannot compare values of types {left?.GetType()} and {right?.GetType()}");
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

            Console.WriteLine($"Cannot compare values of types {left?.GetType()} and {right?.GetType()}");
            Environment.Exit(1);
            return false;
        }

        public static bool IsTrue(object? value)
        {
            if (value is bool b)
                return b;

            Console.WriteLine("Value is not boolean.");
            Environment.Exit(1);
            return false;
        }

        public static bool IsFalse(object? value) => !IsTrue(value);


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

            Console.WriteLine($"Cannot multiply values of types {left?.GetType()} and {right?.GetType()}");
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

            Console.WriteLine($"Cannot divide values of types {left?.GetType()} and {right?.GetType()}");
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

            Console.WriteLine($"Cannot get modulo of types {left?.GetType()} and {right?.GetType()}");
            Environment.Exit(1);
            return null;
        }

        /******************************
            LOGICAL OPERATOR expressions
        *******************************/

        public static object? And(object? left, object? right)
        {
            if (left is bool l && right is bool r)
                return l && r;

            Console.WriteLine($"Cannot perform logical AND on types {left?.GetType()} and {right?.GetType()}");
            Environment.Exit(1);
            return null;
        }

        public static object? Or(object? left, object? right)
        {
            if (left is bool l && right is bool r)
                return l || r;

            Console.WriteLine($"Cannot perform logical OR on types {left?.GetType()} and {right?.GetType()}");
            Environment.Exit(1);
            return null;
        }


        //NOt working as of now, sorry noy huhu
        public static object? Not(object? value)
        {
            if (value is bool b)
                return !b;

            Console.WriteLine($"Cannot perform logical NOT on type {value?.GetType()}");
            return null;
        }
    }
}
