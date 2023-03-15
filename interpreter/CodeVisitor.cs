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

        private bool _isBeginCodeVisited = false;
        private bool _isEndCodeVisited = false;

        public CodeVisitor()
        {
            Variable["DISPLAY:"] = new Func<object?[], object?>(Operator.Display);
            //Variable["SCAN:"] = new Func<object?[], object?>(Scan);
        }

        // TODOS:
        // GOODS 1.) Make a function that will check if the declared variable is comaptible with the data type

        // 2.) Complete the Display function

        // 3.) Make a Scan function

        // 4.) implement the expressions operators
                // Additive - GOODS
                // Comparative - GOODS
                // Multiplicative - GOODS
                // With Parenthesis - GOODS
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

        // PARTIALLY GOODS 13.) Update Code.g4 for BEGIN CODE and END CODE put it in line block
                // Make if block and else if block with begin if and end if
                // separate the begin - code and end - code
    
        /// <summary>
        /// Checks if the delimters are present in the code
        /// </summary>
        /// <returns>nothing if delimters are present, throws an error if they are not present</returns>
        public override object? VisitLineBlock([NotNull] CodeParser.LineBlockContext context)
        {
            if (Evaluator.EvaluateDelimiter(context))
            {
                _isBeginCodeVisited = true;
                _isEndCodeVisited = true;
                return base.VisitLineBlock(context); // Visit the program normally
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


        // TODO: this cannot read INT x, y = 1, z. x and z should be null, currently it is not the case
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
            Evaluator.EvaluateDeclaration(dataType, varNameArray, value);

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
            var value = context.expression() == null ? null : (object?)Visit(context.expression());


            foreach (var name in varNameArray)
            {
                if (!Variable.ContainsKey(name))
                {
                    Console.WriteLine($"Variable {name} is not defined");
                    Environment.Exit(1);
                }

                var dataType = VariableDeclaration[name];
                Evaluator.EvaluateDeclaration(dataType, varNameArray, value);

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

        public override object? VisitParenthesizedExpression([NotNull] CodeParser.ParenthesizedExpressionContext context)
        {
            return Visit(context.expression());
        }

        public override object? VisitAdditiveExpression([NotNull] CodeParser.AdditiveExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.addOp().GetText();

            return op switch
            {
                "+" => Operator.Add(left, right),
                "-" => Operator.Subtract(left, right),
                "&" => Operator.Concatenate(left, right),
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
                "==" => Operator.IsEqual(left, right),
                "<>" => Operator.IsNotEqual(left, right),
                ">" => Operator.GreaterThan(left, right),
                "<" => Operator.LessThan(left, right),
                ">=" => Operator.GreaterThanOrEqual(left, right),
                "<=" => Operator.LessThanOrEqual(left, right),
                _ => throw new NotImplementedException()
            };
        }

        public override object? VisitMultiplicativeExpression([NotNull] CodeParser.MultiplicativeExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.multOp().GetText();

            return op switch
            {
                "*" => Operator.Multiply(left, right),
                "/" => Operator.Divide(left, right),
                "%" => Operator.Modulo(left, right),
                _ => throw new NotImplementedException()
            };
        }

        public override object? VisitBooleanExpression([NotNull] CodeParser.BooleanExpressionContext context)
        {
            var left = Visit(context.expression(0))?.ToString();
            var right = Visit(context.expression(1))?.ToString();

            var op = context.logicOp().LOGICAL_OPERATOR().GetText();

            var parsedLeft = Evaluator.EvaluateBool(left);
            var parsedRight = Evaluator.EvaluateBool(right);

            return op switch
            {
                "AND" => Operator.And(parsedLeft, parsedRight),
                "OR" => Operator.Or(parsedLeft, parsedRight),
                "NOT" => Operator.Not(parsedRight),
                _ => throw new NotImplementedException()
            };
        }
    }
}