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

        public CodeVisitor()
        {
            Variable["DISPLAY:"] = new Func<object?[], object?>(FunctionsOp.Display);
        }

        /// <summary>
        /// Visits the declaration of the grammar and evaluates if the datatype, identifier, and 
        /// its value set is valid. If it is, then it adds the identifier and its value to the
        /// dictionary specific to it.
        /// </summary>
        public override object? VisitDeclaration(CodeParser.DeclarationContext context)
        {
            string dataType = context.dataType().GetText();

            var content = context.children.ToList();

            var varDeclarations = context.IDENTIFIER()
                                 .Select(id => new VariableDeclaration { Name = id.GetText(), Value = null })
                                 .ToList();


            var expressionList = context.expression()?.ToList() ?? new List<CodeParser.ExpressionContext>();
            var valueIndex = 0;

            SemanticErrorEvaluator.EvaluateIsVariableReDeclared(varDeclarations, Variable);

            // Iterate the declaration content
            for (int i = 0; i < content.Count; i++)
            {
                foreach (var varDecl in varDeclarations)
                {
                    if (varDecl.Name == content[i].GetText())
                    {
                        //Check if the variable has an initial value
                        if (i != content.Count - 1 && content[i + 1].GetText() == "=")
                        {
                            if (expressionList.Count > valueIndex)
                            {
                                varDecl.Value = Visit(expressionList[valueIndex]);
                                valueIndex++;
                            }
                        }
                    }

                    Variable[varDecl.Name!] = varDecl.Value;
                    VariableDeclaration[varDecl.Name!] = dataType;

                    SemanticErrorEvaluator.EvaluateDeclaration(dataType, new string[] { varDecl.Name! }, varDecl.Value);
                }
            }

            return null;
        }

        /// <summary>
        /// Visits the assignment of the grammar and evaluates if the identifier is already
        /// defined, or if the identifier's value is valid to the expression assigned to it.
        /// </summary>
        public override object? VisitAssignment([NotNull] CodeParser.AssignmentContext context)
        {
            var varNameArray = context.IDENTIFIER().Select(id => id.GetText()).ToArray();
            var value = Visit(context.expression());

            foreach (var name in varNameArray)
            {
                SemanticErrorEvaluator.EvaluateIsVariableDefined(name, Variable);

                var dataType = VariableDeclaration[name];

                SemanticErrorEvaluator.EvaluateDeclaration(dataType, varNameArray, value);

                Variable[name] = value;
            }
            return null;
        }

        /// <summary>
        /// Visits the identifier and checks the dictionary if the identifier is defined.
        /// If it is already defined, then it throws an error. Otherwise, it stores it to 
        /// the dictionary assigned to it.
        /// </summary>
        public override object? VisitIdentifierExpression(CodeParser.IdentifierExpressionContext context)
        {
            var varName = context.IDENTIFIER().GetText();

            SemanticErrorEvaluator.EvaluateIsVariableDefined(varName, Variable);

            return Variable[varName];
        }

        /// <summary>
        /// Visits the constant and identifies the context value 
        /// and parses it to its corresponding data type.
        /// </summary>
        public override object? VisitConstant([NotNull] CodeParser.ConstantContext context)
        {
            if (context.INTEGER_VAL() is { } i)
                return int.Parse(i.GetText());

            if (context.FLOAT_VAL() is { } f)
                return float.Parse(f.GetText());

            if (context.BOOL_VAL() is { } b)
                return b.GetText().Equals("\"TRUE\"");

            if (context.STRING_VAL() is { } s)
                return s.GetText()[1..^1];

            if (context.CHAR_VAL() is { } c)
                return c.GetText()[1];

            Console.WriteLine("SEMANTIC ERROR: Unknown value type.");
            Environment.Exit(400);
            return null;
        }

        /// <summary>
        /// Visits the function call and checks if the function is defined.
        /// If not, then it throws an error. Otherwise it evaluates the function
        /// based on the function name.
        /// </summary>
        public override object? VisitFunctionCall([NotNull] CodeParser.FunctionCallContext context)
        {
            string name = context.DISPLAY() != null ? "DISPLAY:" : "SCAN:";

            var args = context.expression() == null ? null : Visit(context.expression());
            var varNameArray = context.IDENTIFIER().Select(id => id.GetText()).ToArray();

            return name switch
            {
                "DISPLAY:" => FunctionsOp.Display(args),
                "SCAN:" => FunctionsOp.Scan(varNameArray, Variable, VariableDeclaration),
                _ => SemanticErrorEvaluator.EvaluateIsFunctionDefined(name, Variable)
            };
        }

        /// <summary>
        /// Visits the parenthesized expression 
        /// E.G. ( )
        /// and applies the rule to it 
        /// </summary>
        public override object? VisitParenthesizedExpression([NotNull] CodeParser.ParenthesizedExpressionContext context)
        {
            return Visit(context.expression());
        }

        /// <summary>
        /// Visits the expression and evaluates the Additive expression based on the operator.
        /// </summary>
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

        /// <summary>
        /// Visits the expression and evaluates the Comparative expression based on the operator.
        /// </summary>
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

        /// <summary>
        /// Visits the expression and evaluates the Multiplicative expression
        /// based on the operator.
        /// </summary>
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

        /// <summary>
        /// Visits the expression and evaluates the Logical expression based on the operator.
        /// </summary>
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

        /// <summary>
        /// Visits the expression and returns the opposite value of the boolean inputted.
        /// </summary>
        public override object? VisitNotExpression([NotNull] CodeParser.NotExpressionContext context)
        {
            return Logical.Not(Visit(context.expression()));
        }

        /// <summary>
        /// Adds next line to the output.
        /// </summary>
        public override object? VisitNextLineExpression([NotNull] CodeParser.NextLineExpressionContext context)
        {
            return "\n";
        }

        /// <summary>
        /// Visits the expression and evaluates the Unary expression based on the operator.
        /// </summary>
        public override object? VisitUnaryExpression([NotNull] CodeParser.UnaryExpressionContext context)
        {
            string symbol = context.unary().GetText();
            var expressionValue = Visit(context.expression());

            return Unary.UnaryValue(symbol, expressionValue);
        }

        /// <summary>
        /// Visits the expression and evaluates concatenates left and right values.
        /// </summary>
        public override object? VisitConcatExpression([NotNull] CodeParser.ConcatExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            return Concat.Concatenate(left, right);
        }

        /// <summary>
        /// Visits the while block and evaluates the condition and the body.
        /// </summary>
        public override object? VisitWhileBlock([NotNull] CodeParser.WhileBlockContext context)
        {
            Func<object?, bool> condition = context.WHILE().GetText() == "WHILE"
               ? SemanticErrorEvaluator.IsTrue
               : SemanticErrorEvaluator.IsFalse
           ;

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

        /// <summary>
        /// Visits the if block and evaluates the condition and the body.
        /// It also evaluates the else if and else blocks if the condition of 
        /// the if block is false.
        /// </summary>
        public override object? VisitIfBlock([NotNull] CodeParser.IfBlockContext context)
        {
            var condition = SemanticErrorEvaluator.IsTrue(Visit(context.expression()));

            if (condition == true)
            {
                foreach (var line in context.line())
                {
                    Visit(line);
                }
                return null;
            }
          
            foreach (var elseIf in context.elseIfBlock())
            {
                var elseIfCondition = SemanticErrorEvaluator.IsTrue(Visit(elseIf.expression()));

                if (elseIfCondition)
                {
                    foreach (var line in elseIf.line())
                    {
                        Visit(line);
                    }
                    return null;
                }
            }

            if (context.elseBlock() != null)
            {
                var elseBlock = context.elseBlock();
                
                foreach (var line in elseBlock.line())
                {
                    Visit(line);
                }
            }
            return null;
        }

        /// <summary>
        /// Visits the switchCaseBlock and evaluates the condition and the body.
        /// It also evaluates the case and default blocks.
        /// </summary>
        public override object? VisitSwitchCaseBlock([NotNull] CodeParser.SwitchCaseBlockContext context)
        {
            var switchExpression = Visit(context.expression());

            foreach (var caseBlockContext in context.caseBlock())
            {
                var expression = Visit(caseBlockContext.expression());

                if (FunctionsOp.GetSwitchCaseBool(expression, switchExpression))
                {
                    foreach (var line in caseBlockContext.line())
                    {
                        Visit(line);
                    }
                    return null;
                }

            }

            if (context.defaultBlock() != null)
            {
                foreach (var line in context.defaultBlock().line())
                {
                    Visit(line);
                }

            }
            return null;
        }
    }
}