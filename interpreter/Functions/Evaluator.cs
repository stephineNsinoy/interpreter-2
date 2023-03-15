using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using interpreter.Grammar;

namespace interpreter
{
    public class Evaluator
    {
        private static bool EvaluateDataType(string dataType, object? value)
        {
            if (value == null)
            {
                return true; // Null can be assigned to any data type
            }

            switch (dataType)
            {
                case "INT":
                    return value is int;
                case "FLOAT":
                    return value is float;
                case "BOOL":
                    //checks the string value, parses it into boolean, and returns
                    //it as a boolean depending on what the user input
                    string? boolStr = value as string;
                    bool? parsedStr = EvaluateBool(boolStr);
                    return parsedStr != null;
                case "STRING":
                    return value is string;
                case "CHAR":
                    return value is char;
                default:
                    return false; // Invalid data type
            }
        }

        public static void EvaluateDeclaration(string dataType, string[] varNames, object? value)
        {
            bool isValid = true;

            foreach (var name in varNames)
            {
                if (!EvaluateDataType(dataType, value))
                {
                    Console.WriteLine($"Invalid value assigned to variable {name}");
                    isValid = false;
                }
            }
            if (!isValid)
            {
                Environment.Exit(1);
            }
        }

        public static bool EvaluateDelimiter(CodeParser.LineBlockContext context)
        {
            string beginDelimiter = "BEGIN CODE";
            string endDelimiter = "END CODE";
            string? beginCode = context.BEGIN_CODE()?.GetText();
            string? endCode = context.END_CODE()?.GetText();

            //Both delimiters are present in the code
            if (beginCode != null && endCode != null && beginCode.Equals(beginDelimiter) && endCode.Equals(endDelimiter))
            {
                // Visit the declarations and lines only if the delimiters are at the beginning and end of the program
                if (context.declaration().Length == 0 && context.line().Length == 0)
                {
                    Console.WriteLine("CODE not recognized \n" +
                                       "Delimiters must be in the right position");
                    Environment.Exit(1);
                }

                // Declaration contains BEGIN CODE
                if (context.declaration().Length > 0 && context.declaration().Any(d => d.GetText().Contains(beginDelimiter)))
                {
                    Console.WriteLine($"{beginDelimiter} must only be at the beginning of the program");
                    Environment.Exit(1);
                }

                // Line contains BEGIN CODE
                if (context.line().Length > 0 && context.line().Any(l => l.GetText().Contains(beginDelimiter)))
                {
                    Console.WriteLine($"{beginDelimiter} must be only at the beginning of the program");
                    Environment.Exit(1);
                }

                //_isBeginCodeVisited = true;

                // NOT WORKING
                // Declaration contains END CODE
                if (context.declaration().Length > 0 && context.declaration().Any(d => d.GetText().Contains(endDelimiter) && d.GetText().IndexOf(endDelimiter) > 0))
                {
                    Console.WriteLine($"{endDelimiter} must be only at the end of the program");
                    Environment.Exit(1);
                }

                // Line contains END CODE
                if (context.line().Length > 0 && context.line().Any(l => l.GetText().Contains(endDelimiter) && l.GetText().IndexOf(endDelimiter) < l.GetText().Length - endDelimiter.Length))
                {
                    Console.WriteLine($"{endDelimiter} must be only at the end of the program");
                    Environment.Exit(1);
                }
               
                return true;
            }
            else if ((beginCode == null || !beginCode.Equals(beginDelimiter)) && (endCode != null && endCode.Equals(endDelimiter)))
            {
                // Only the end delimiter is present
                Console.WriteLine("Missing BEGIN CODE delimiter");
                Environment.Exit(1);
            }

            else if ((beginCode != null && beginCode.Equals(beginDelimiter)) && (endCode == null || !endCode.Equals(endDelimiter)))
            {
                // Only the begin delimiter is present
                Console.WriteLine("Missing END CODE delimiter");
                Environment.Exit(1);
            }

            else
            {
                // Neither delimiter is present
                Console.WriteLine("Missing delimiters");
                Environment.Exit(1);
            }

            return false;
        }

        public static bool? EvaluateBool(string? str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            if (str.Equals("TRUE", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (str.Equals("FALSE", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return null;
        }
    }
}
