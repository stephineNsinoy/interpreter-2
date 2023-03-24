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

        public override object? VisitLineBlock([NotNull] CodeParser.LineBlockContext context)
        {
            if (Evaluator.EvaluateCodeDelimiter(context))
            {
                _isBeginCodeVisited = true;
                _isEndCodeVisited = true;
                return base.VisitLineBlock(context); // Visit the program normally
            }

            return null;
        }

        // IN-PROGRESS
        public override object? VisitLine([NotNull] CodeParser.LineContext context)
        {
            Evaluator.EvaluateNewLine(context.GetText());
            return base.VisitLine(context);
        }

        /// <summary>
        /// Still needs to be double checked, test for all possbile cases
        /// </summary>
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
            Evaluator.EvaluateNotValidDeclaration(context);

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

        public override object? VisitConstant([NotNull] CodeParser.ConstantContext context)
        {
            if (context.INTEGER_VAL() is { } i)
                return int.Parse(i.GetText());

            if (context.FLOAT_VAL() is { } f)
                return float.Parse(f.GetText());

            if (context.STRING_VAL() is { } s)
            {
                if(s.GetText().Equals("\"TRUE\"") || s.GetText().Equals("\"FALSE\""))
                    return Evaluator.EvaluateBool(s.GetText());
                
                else
                    return s.GetText()[1..^1];
            }

            if (context.CHAR_VAL() is { } c)
                return c.GetText()[1];

            Console.WriteLine("Unknown value type.");
            Environment.Exit(1);
            return null;
        }

        public override object? VisitFunctionCall([NotNull] CodeParser.FunctionCallContext context)
        {
            Evaluator.EvaluateAfterBeginCodeStatements(!_isBeginCodeVisited);

            var name = context.DISPLAY() != null ? "DISPLAY:" : "SCAN:";
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
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));
            
            var op = context.logicOp().LOGICAL_OPERATOR().GetText();
            
            return op switch
            {
                "AND" => Operator.And(left, right),
                "OR" => Operator.Or(left, right),
                _ => throw new NotImplementedException()
            };
        }

        public override object? VisitNotExpression([NotNull] CodeParser.NotExpressionContext context)
        {
            return Operator.Not(Visit(context.expression()));
        }

        public override object? VisitNextLineExpression([NotNull] CodeParser.NextLineExpressionContext context)
        { 
            return "\n";
        }

        public override object? VisitUnaryExpression([NotNull] CodeParser.UnaryExpressionContext context)
        {
            string symbol = context.unary().GetText();
            var expressionValue = Visit(context.expression());

            return Operator.UnaryValue(symbol, expressionValue);
        }

        public override object? VisitConcatExpression([NotNull] CodeParser.ConcatExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            return Operator.Concatenate(left, right);
        }

        // IN-PROGRESS
        public override object? VisitWhileBlock([NotNull] CodeParser.WhileBlockContext context)
        {
            Func<object?, bool> condition = context.WHILE().GetText() == "WHILE"
               ? Operator.IsTrue
               : Operator.IsFalse
           ;

            Evaluator.EvaluateWhileDelimiter(context);

            if (condition(Visit(context.expression())))
            {
                do
                {
                    foreach (var line in context.line())
                    {
                        Visit(line);
                    }
                } while (condition(Visit(context.expression())));
            }

            return base.VisitWhileBlock(context);
        }
    }
}