using Antlr4.Runtime.Misc;
using interpreter.Functions;
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
        private Dictionary<string, object?> Variable { get; } = new();
        private Dictionary<string, string> VariableDeclaration { get; } = new Dictionary<string, string>();
        private Evaluator _evaluator;
        private Operator _op;
        private bool _isBeginCodeVisited = false;
        private bool _isEndCodeVisited = false;

        public CodeVisitor()
        {
            _evaluator = new Evaluator();
            _op = new Operator();

            Variable["DISPLAY:"] = new Func<object?[], object?>(_op.Display);
            //Variable["SCAN:"] = new Func<object?[], object?>(Scan);
        }

        // TODOS:
        // GOODS 1.) Make a function that will check if the declared variable is comaptible with the data type

        // 2.) Complete the Display function

        // 3.) Make a Scan function

        // 4.) implement the expressions operators
                // Additive - GOODS
                // Comparative - GOODS
                // Multiplicative
                // Logical
                // Unary

        // 5.) Test if begin and end code cannot be placed anywhere

        // GOODS 6.) This declaration is correct: INT y --- apply logic that will accept this declaration

        // GOODS 7.) Assignment still not supports x=y=5

        // 8.) Handle error for declaring more than one character inside single quote ex. 'dada'

        // 9.) Cannot read boolean values because it will be stored as a string

        // GOODS 10.) Error handler when assigning values to a variable not conforming to data type

        // 11.) Error handler if there are declarations or lines after END CODE

        // 12.) Implement the escape code []

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

                _isBeginCodeVisited = true;

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

                _isEndCodeVisited = true;

                return base.VisitProgram(context); // Visit the program normally
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

            return null;
        }

        // NOT WORKING -- checks if there is a declaration in a line

        //public override object? VisitLine([NotNull] CodeParser.LineContext context)
        //{
        //    if (ContainsDeclaration(context))
        //    {
        //        Console.WriteLine("Error: Declaration found in line where it is not allowed.");
        //        Environment.Exit(1);
        //    }

        //    return base.VisitLine(context);
        //}

        //private bool ContainsDeclaration([NotNull] CodeParser.LineContext context)
        //{
        //    if (context.declaration() != null)
        //    {
        //        return true;
        //    }

        //    foreach (var child in context.children)
        //    {
        //        if (child is CodeParser.LineContext line)
        //        {
        //            if (ContainsDeclaration(line))
        //            {
        //                return true;
        //            }
        //        }
        //    }

        //    return false;
        //}

        public override object? VisitDeclaration(CodeParser.DeclarationContext context)
        {
            if (!_isBeginCodeVisited)
            {
                Console.WriteLine("Declaration must be placed after BEGIN CODE");
                Environment.Exit(1);
            }

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
            _evaluator.CheckDeclaration(dataType, varNameArray, value);

            return null;
        }

        public override object? VisitAssignment([NotNull] CodeParser.AssignmentContext context)
        {
            if (!_isBeginCodeVisited)
            {
                Console.WriteLine("Assignment must be placed after BEGIN CODE");
                Environment.Exit(1);
            }

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
                _evaluator.CheckDeclaration(dataType, varNameArray, value);

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
            if (!_isBeginCodeVisited)
            {
                Console.WriteLine("Function call must be placed after BEGIN CODE");
                Environment.Exit(1);
            }

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

        public override object? VisitAdditiveExpression([NotNull] CodeParser.AdditiveExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.addOp().GetText();

            return op switch
            {
                "+" => _op.Add(left, right),
                "-" => _op.Subtract(left, right),
                "&" => _op.Concatenate(left, right),
                _ => throw new NotImplementedException()
            };
        }

        public override object? VisitComparativeExpression([NotNull] CodeParser.ComparativeExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.compareOp().GetText();

            return op switch
            {
                "==" => _op.IsEqual(left, right),
                "<>" => _op.IsNotEqual(left, right),
                ">" => _op.GreaterThan(left, right),
                "<" => _op.LessThan(left, right),
                ">=" => _op.GreaterThanOrEqual(left, right),
                "<=" => _op.LessThanOrEqual(left, right),
                _ => throw new NotImplementedException()
            };
        }
    }
}