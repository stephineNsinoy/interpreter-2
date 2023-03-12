using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpreter.Functions
{
    public class Operator
    {
        public object? Display(object?[] args)
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
        public object? Add(object? left, object? right)
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

        public object? Subtract(object? left, object? right)
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

        public object? Concatenate(object? left, object? right)
        {
            return $"{left}{right}";

            //Console.WriteLine($"Cannot concatenate values of types {left?.GetType()} and {right?.GetType()}");
            //Environment.Exit(1);
            //return null;
        }


        /******************************
            Comparative expressions
        *******************************/
        public bool IsEqual(object? left, object? right)
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

        public bool IsNotEqual(object? left, object? right)
        {
            return !IsEqual(left, right);
        }

        public bool GreaterThan(object? left, object? right)
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

        public bool LessThan(object? left, object? right)
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

        public bool GreaterThanOrEqual(object? left, object? right)
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

        public bool LessThanOrEqual(object? left, object? right)
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

        public bool IsTrue(object? value)
        {
            if (value is bool b)
                return b;

            Console.WriteLine("Value is not boolean.");
            Environment.Exit(1);
            return false;
        }

        public bool IsFalse(object? value) => !IsTrue(value);
    }
}
