using Antlr4.Runtime.Misc;
using interpreter.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpreter
{
    public class CodeVisitor : CodeBaseVisitor<object?>
    {
        Dictionary<string, object?> Variable { get; } = new();
        Dictionary<string, string> VariableDeclaration { get; } = new Dictionary<string, string>();

        public CodeVisitor()
        {
            Variable["DISPLAY:"] = new Func<object?[], object?>(Display);
            //Variable["SCAN:"] = new Func<object?[], object?>(Scan);
        }

        // TODOS:
        // GOODS 1.) Make a function that will check if the declared variable is comaptible with the data type

        // 2.) Complete the Display function

        // 3.) Make a Scan function

        // 4.) Test the expressions

        // 5.) Test if begin and end code cannot be placed anywhere

        // GOODS 6.) This declaration is correct: INT y --- apply logic that will accept this declaration

        // GOODS 7.) Assignment still not supports x=y=5

        // 8.) Handle error for declaring more than one character inside single quote ex. 'dada'

        // 9.) Cannot read boolean values because it will be stored as a string

        // GOODS 10.) Error handler when assigning values to a variable not conforming to data type

        private object? Display(object?[] args)
        {
            foreach (var arg in args)
            {
                Console.Write(arg);
            }

            return null;
        }

        // Checks if declared variable value conforms with the data type
        //public void CheckDeclaration(string dataType, string[] varName, object? value)
        //{
        //    bool isValid = true;
        //    foreach (string name in varName)
        //    {
        //        if (dataType == "INT")
        //        {
        //            if (value is int || value is null)
        //            {
        //                // do nothing
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Variable {name} is not an integer");
        //                isValid = false;
        //            }
        //        }
        //        else if (dataType == "FLOAT")
        //        {
        //            if (value is float || value is null)
        //            {
        //                // do nothing
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Variable {name} is not a float");
        //                isValid = false;
        //            }
        //        }
        //        else if (dataType == "STRING")
        //        {
        //            if (value is string || value is null)
        //            {
        //                // do nothing
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Variable {name} is not a string");
        //                isValid = false;
        //            }
        //        }
        //        else if (dataType == "CHAR")
        //        {
        //            if (value is char || value is null)
        //            {
        //                // do nothing
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Variable {name} is not a character");
        //                isValid = false;
        //            }
        //        }
        //        else if (dataType == "BOOL")
        //        {
        //            if (value is bool || value is null)
        //            {
        //                // do nothing
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Variable {name} is not a boolean");
        //                isValid = false;
        //            }
        //        }
        //    }

        //    if (!isValid)
        //    {
        //        Environment.Exit(1);
        //    }
        //}

        private bool CheckDataType(string dataType, object? value)
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
                    return value is bool;
                case "STRING":
                    return value is string;
                case "CHAR":
                    return value is char;
                default:
                    return false; // Invalid data type
            }
        }

        private void CheckDeclaration(string dataType, string[] varNames, object? value)
        {
            bool isValid = true;

            foreach (var name in varNames)
            {
                if (!CheckDataType(dataType, value))
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

        /// <summary>
        /// Checks if the delimters are present in the code
        /// </summary>
        /// <returns>nothing if delimters are present, throws an error if they are not present</returns>
        public override object? VisitProgram([NotNull] CodeParser.ProgramContext context)
        {
            string beginDelimiter = "BEGIN CODE";
            string endDelimiter = "END CODE";
            string? beginCode = context.BEGIN_CODE()?.GetText();
            string? endCode = context.END_CODE()?.GetText();

            if (beginCode != null && endCode != null && beginCode.Equals(beginDelimiter) && endCode.Equals(endDelimiter))
            {
                // Both delimiters are present in the code
                return base.VisitProgram(context); // Visit the program normally
            }
            else if ((beginCode == null || !beginCode.Equals(beginDelimiter)) && (endCode != null && endCode.Equals(endDelimiter)))
            {
                // Only the end delimiter is present
                Console.WriteLine("Missing BEGIN CODE delimiter");
            }
            else if ((beginCode != null && beginCode.Equals(beginDelimiter)) && (endCode == null || !endCode.Equals(endDelimiter)))
            {
                // Only the begin delimiter is present
                Console.WriteLine("Missing END CODE delimiter");
            }
            else
            {
                // Neither delimiter is present
                Console.WriteLine("Missing delimiters");
            }
            return null;
        }

        public override object? VisitDeclaration(CodeParser.DeclarationContext context)
        {
            string dataType = context.dataType().GetText();

            var varNameArray = context.IDENTIFIER().Select(id => id.GetText()).ToArray();

            // Check if value is null or expression is missing so that (INT y) will be accepted
            var value = context.expression() == null ? null : (object?)Visit(context.expression());

            foreach (var name in varNameArray)
            {
                Variable[name] = value;
                VariableDeclaration[name] = dataType;
            }

            // Check if the value assigned to the variable conforms with the data type
            CheckDeclaration(dataType, varNameArray, value);

            return null;
        }

        public override object? VisitAssignment([NotNull] CodeParser.AssignmentContext context)
        {
            var varNameArray = context.IDENTIFIER().Select(id => id.GetText()).ToArray();
            var value = Visit(context.expression());

            foreach (var name in varNameArray)
            {
                if (!Variable.ContainsKey(name))
                {
                    Console.WriteLine($"Variable {name} is not defined");
                    Environment.Exit(1);
                }

                var dataType = VariableDeclaration[name];
                CheckDeclaration(dataType, varNameArray, value);

                Variable[name] = value;
            }

            return null;
        }

        public override object? VisitIdentifierExpression(CodeParser.IdentifierExpressionContext context)
        {
            var varName = context.IDENTIFIER().GetText();

            if (!Variable.ContainsKey(varName))
            {
                Console.WriteLine($"Variable {varName} is not defined");
                Environment.Exit(1);
            }

            return Variable[varName];
        }

        public override object VisitConstant([NotNull] CodeParser.ConstantContext context)
        {
            if (context.INTEGER_VAL() is { } i)
                return int.Parse(i.GetText());

            if (context.FLOAT_VAL() is { } f)
                return float.Parse(f.GetText());

            if (context.BOOL_VAL() is { } b)
                return b.GetText() == "\"TRUE\"";

            if (context.STRING_VAL() is { } s)
                return s.GetText()[1..^1];

            if (context.CHAR_VAL() is { } c)
                return c.GetText()[1];



            throw new Exception("Unknown value type.");
        }

        public override object? VisitFunctionCall([NotNull] CodeParser.FunctionCallContext context)
        {
            var name = context.FUNCTIONS().GetText();
            var args = context.expression().Select(Visit).ToArray();

            if (!Variable.ContainsKey(name))
            {
                throw new Exception($"Function {name} is not defined.");
            }

            if (Variable[name] is not Func<object?[], object?> func)
            {
                throw new Exception($"Variable {name} is not a function.");
            }

            return func(args);
        }

        //TODO
        //visit unknown is not recognized by the program    
        public override object? VisitUnknown([NotNull] CodeParser.UnknownContext context)
        {
            var blank_line = context.BLANK_LINE().GetText();

            var colon = context.SEMI_COLON().GetText();

            Console.WriteLine(blank_line);

            if (blank_line != null)
            {
                Console.WriteLine("Every line must contain a single statement");
            }
            if (colon != null)
            {
                Console.WriteLine("\';\' is not a valid statement");
            }

            return null;
        }
    }
}