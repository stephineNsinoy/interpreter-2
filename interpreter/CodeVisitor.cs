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
        Dictionary<string, Variables?> VariableDeclaration  { get; } = new();

        // TODO:
        // Make a function that will check if the declared variable is comaptible with the data type

        //public override object VisitProgram([NotNull] CodeParser.ProgramContext context)
        //{
        //    var begin = context.BEGIN_CODE().GetText();
        //    return base.VisitProgram(context);
        //}
     
        public override object? VisitDeclaration(CodeParser.DeclarationContext context)
        {
            string dataType = context.dataType().GetText();

            var varNameArray = context.IDENTIFIER().Select(id => id.GetText()).ToArray();

            var value = Visit(context.expression());

            var newVariable = new Variables();

            foreach (var name in varNameArray)
            {
                newVariable[name] = value;
            }

            VariableDeclaration[dataType] = newVariable;

            var iValue =  VariableDeclaration["INT"];
            //var sample = iValue[0];

            return null;
        }

        public override object? VisitAssignment([NotNull] CodeParser.AssignmentContext context)
        {
            var varName = context.IDENTIFIER().GetText();
            var value = Visit(context.expression());

            Variable[varName] = value;
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

        public override object VisitConstant([NotNull] CodeParser.ConstantContext context)
        {
            if (context.INTEGER_VAL() is { } i)
                return int.Parse(i.GetText());

            if (context.FLOAT_VAL() is { } f)
                return float.Parse(f.GetText());

            if (context.STRING_VAL() is { } s)
                return s.GetText()[1..^1];

            if (context.BOOL_VAL() is { } b)
                return b.GetText() == "\"TRUE\"";

            if (context.CHARACTER_VAL() is { } c)
                return c.GetText()[0];

            throw new Exception("Unknown value type.");
        }
    }
}
