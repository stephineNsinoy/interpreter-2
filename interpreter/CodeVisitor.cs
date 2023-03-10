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
        Dictionary<string, object?> Variables { get; } = new();
        Dictionary<string, Variables?> VariableDeclaration  { get; } = new();
            
        public override object? VisitDeclaration( CodeParser.DeclarationContext context)
        {
            var dataType = context.dataType().GetText();

            var varNameArray = context.IDENTIFIER().Select(Visit).ToArray();

            var value = Visit(context.expression());

            foreach (var name in varNameArray)
            {
                //var namedValues = new Variables();
                //namedValues[name.] = value;
                //Variable.Add(name, value);

                
            }

            return null;
        }

        public override object? VisitIdentifierExpression(CodeParser.IdentifierExpressionContext context)
        {
            var varName = context.IDENTIFIER().GetText();

            if (!Variable.ContainsKey(varName))
            {
                throw new Exception($"Variable {varName} is not defined");
            }

            return Variable[varName];
        }
    }
}
