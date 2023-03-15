using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using interpreter.Functions;
using interpreter.Grammar;
using interpreter.Variables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace interpreter
{
    public class CodeVisitor : CodeBaseVisitor<object?>
    {
        // Variable name and its value
        private Dictionary<string, object?> Variable { get; } = new();

        // Variable name and its data type
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
                    // Error handler if there is a BEGIN CODE inside or outside
                    // Error handler if there are declarations or lines after END 
            // GOODS 6.) INT y,x --- apply logic that will accept this declaration
            // GOODS 7.) Assignment still not supports x=y=5
            // 8.) Handle error for declaring more than one character inside single quote ex. 'dada'
            // 9.) Cannot read boolean values because it will be stored as a string
            // GOODS 10.) Error handler when assigning values to a variable not conforming to data type
            // 11.) Implement the escape code []
            // PARTIALLY GOODS 12.) Update Code.g4 for BEGIN CODE and END CODE put it in line block
            // 12.) IF ELSE ELSEIF statement implementation
            // 13.) WHILE statement implementation
            // 14.) CHECK THE EVALUATOR CLASS -- CHECK LISTED TODOS IN COMMENTS

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


        public override object? VisitDeclaration(CodeParser.DeclarationContext context)
        {
            Evaluator.EvaluateAfterBeginCodeStatements(!_isBeginCodeVisited);

            string dataType = context.dataType().GetText();

            var content = context.children.ToList();

            var varDeclarations = context.IDENTIFIER()
                                 .Select(id => new VariableDeclaration { Name = id.GetText(), Value = null })
                                 .ToList();

            Evaluator.EvaluateDeclarationContent(varDeclarations, content);

            var expressionList = context.expression()?.ToList() ?? new List<CodeParser.ExpressionContext>();
            var valueIndex = 0;

            Evaluator.EvaluateIsVariableReDeclared(varDeclarations, Variable);

            // Iterate the declaration content
            for (int i = 0; i < content.Count(); i++)
            {
                foreach (var varDecl in varDeclarations)
                {
                    if (varDecl.Name == content[i].GetText())
                    {
                        Evaluator.EvaluateIsDeclarationValid(content[i - 1].GetText());

                        //Check if the variable has an initial value
                        if (i != content.Count() - 1 && content[i + 1].GetText() == "=")
                        {
                            Evaluator.EvaluateIsValuePresent(expressionList[valueIndex].GetText());

                            if (expressionList.Count > valueIndex)
                            {
                                varDecl.Value = Visit(expressionList[valueIndex]);
                                valueIndex++;
                            }
                        }
                    }

                    Variable[varDecl.Name!] = varDecl.Value;
                    VariableDeclaration[varDecl.Name!] = dataType;

                    Evaluator.EvaluateDeclaration(dataType, new string[] { varDecl.Name! }, varDecl.Value);
                }
            }

            return null;
        }


        public override object? VisitAssignment([NotNull] CodeParser.AssignmentContext context)
        {
            Evaluator.EvaluateAfterBeginCodeStatements(!_isBeginCodeVisited);

            var varNameArray = context.IDENTIFIER().Select(id => id.GetText()).ToArray();
            var value = context.expression() == null ? null :(object?)Visit(context.expression());

            foreach (var name in varNameArray)
            {
                Evaluator.EvaluateIsVariableDefined(name, Variable);
            
                var dataType = VariableDeclaration[name];

                Evaluator.EvaluateDeclaration(dataType, varNameArray, value);

                Variable[name] = value;
            }

            Evaluator.EvaluateAssignment(varNameArray, value);

            return null;
        } 

        public override object? VisitIdentifierExpression(CodeParser.IdentifierExpressionContext context)
        {
            var varName = context.IDENTIFIER().GetText();

            Evaluator.EvaluateIsVariableDefined(varName, Variable);

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

            Console.WriteLine("Unknown value type.");
            Environment.Exit(1);
            return null;
        }

        public override object? VisitFunctionCall([NotNull] CodeParser.FunctionCallContext context)
        {
            Evaluator.EvaluateAfterBeginCodeStatements(!_isBeginCodeVisited);

            var name = context.FUNCTIONS().GetText();
            var args = context.expression().Select(Visit).ToArray();

            try
            {
                Evaluator.EvaluateIsFunctionDefined(name, Variable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
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
    }
}