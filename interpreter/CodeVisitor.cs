using Antlr4.Runtime.Misc;
using interpreter.Functions.Evaluators;
using interpreter.Functions.Operators;
using interpreter.Functions.Operators.Expressions;
using interpreter.Grammar;
using interpreter.Variables;

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
            Variable["DISPLAY:"] = new Func<object?[], object?>(FunctionsOp.Display);
            //Variable["SCAN:"] = new Func<object?[], object?>(Scan);
        }

        public override object? VisitLineBlock([NotNull] CodeParser.LineBlockContext context)
        {
            if (Delimiter.EvaluateCodeDelimiter(context))
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
            Statement.EvaluateNewLine(context.NEWLINE()?.GetText());
            return base.VisitLine(context);
        }

        /// <summary>
        /// Still needs to be double checked, test for all possbile cases
        /// </summary>
        public override object? VisitDeclaration(CodeParser.DeclarationContext context)
        {
            Delimiter.EvaluateAfterBeginCodeStatements(!_isBeginCodeVisited);
            Statement.EvaluateNewLine(context.NEWLINE()?.GetText());

            string dataType = context.dataType().GetText();

            var content = context.children.ToList();

            var varDeclarations = context.IDENTIFIER()
                                 .Select(id => new VariableDeclaration { Name = id.GetText(), Value = null })
                                 .ToList();


            Declaration.EvaluateDeclarationContent(varDeclarations, content);

            var expressionList = context.expression()?.ToList() ?? new List<CodeParser.ExpressionContext>();
            var valueIndex = 0;

            Declaration.EvaluateIsVariableReDeclared(varDeclarations, Variable);

            // Iterate the declaration content
            for (int i = 0; i < content.Count; i++)
            {
                foreach (var varDecl in varDeclarations)
                {
                    if (varDecl.Name == content[i].GetText())
                    {
                        Declaration.EvaluateIsDeclarationValid(content[i - 1].GetText());

                        //Check if the variable has an initial value
                        if (i != content.Count - 1 && content[i + 1].GetText() == "=")
                        {
                            Declaration.EvaluateIsValuePresent(expressionList[valueIndex].GetText());

                            if (expressionList.Count > valueIndex)
                            {
                                varDecl.Value = Visit(expressionList[valueIndex]);
                                valueIndex++;
                            }
                        }
                    }

                    Variable[varDecl.Name!] = varDecl.Value;
                    VariableDeclaration[varDecl.Name!] = dataType;

                    Declaration.EvaluateDeclaration(dataType, new string[] { varDecl.Name! }, varDecl.Value);
                }
            }

            return null;
        }

        public override object? VisitAssignment([NotNull] CodeParser.AssignmentContext context)
        {
            Delimiter.EvaluateAfterBeginCodeStatements(!_isBeginCodeVisited);
            Declaration.EvaluateNotValidDeclaration(context);

            var varNameArray = context.IDENTIFIER().Select(id => id.GetText()).ToArray();
            var value = context.expression() == null ? null :(object?)Visit(context.expression());

            foreach (var name in varNameArray)
            {
                Assignment.EvaluateIsVariableDefined(name, Variable);
            
                var dataType = VariableDeclaration[name];

                Assignment.EvaluateAssignment(varNameArray, value, context.GetText());
                Declaration.EvaluateDeclaration(dataType, varNameArray, value);

                Variable[name] = value;
            }

            return null;
        } 

        public override object? VisitIdentifierExpression(CodeParser.IdentifierExpressionContext context)
        {
            var varName = context.IDENTIFIER().GetText();

            Assignment.EvaluateIsVariableDefined(varName, Variable);

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
                    return FunctionsOp.GetBool(s.GetText());
                
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
            Delimiter.EvaluateAfterBeginCodeStatements(!_isBeginCodeVisited);

            var name = context.DISPLAY() != null ? "DISPLAY:" : "SCAN:";
            var args = context.expression().Select(Visit).ToArray();

            try
            {
                Function.EvaluateIsFunctionDefined(name, Variable);
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
                "+" => Additive.Add(left, right),
                "-" => Additive.Subtract(left, right),
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
                "==" => Comparative.IsEqual(left, right),
                "<>" => Comparative.IsNotEqual(left, right),
                ">" => Comparative.GreaterThan(left, right),
                "<" => Comparative.LessThan(left, right),
                ">=" => Comparative.GreaterThanOrEqual(left, right),
                "<=" => Comparative.LessThanOrEqual(left, right),
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
                "*" => Multiplicative.Multiply(left, right),
                "/" => Multiplicative.Divide(left, right),
                "%" => Multiplicative.Modulo(left, right),
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
                "AND" => Logical.And(left, right),
                "OR" => Logical.Or(left, right),
                _ => throw new NotImplementedException()
            };
        }

        public override object? VisitNotExpression([NotNull] CodeParser.NotExpressionContext context)
        {
            return Logical.Not(Visit(context.expression()));
        }

        public override object? VisitNextLineExpression([NotNull] CodeParser.NextLineExpressionContext context)
        { 
            return "\n";
        }

        public override object? VisitUnaryExpression([NotNull] CodeParser.UnaryExpressionContext context)
        {
            string symbol = context.unary().GetText();
            var expressionValue = Visit(context.expression());

            return Unary.UnaryValue(symbol, expressionValue);
        }

        public override object? VisitConcatExpression([NotNull] CodeParser.ConcatExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            return Concat.Concatenate(left, right);
        }

        // IN-PROGRESS
        public override object? VisitWhileBlock([NotNull] CodeParser.WhileBlockContext context)
        {
            Func<object?, bool> condition = context.WHILE().GetText() == "WHILE"
               ? Condition.IsTrue
               : Condition.IsFalse
           ;

            Delimiter.EvaluateWhileDelimiter(context);

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