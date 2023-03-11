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

        public CodeVisitor()
        {
            Variable["DISPLAY:"] = new Func<object?[], object?>(Display);
        }

        // TODO:
        // 1.) Make a function that will check if the declared variable is comaptible with the data type
        // VariableDeclaration[dataType] = newVariable;

        // var iValue = VariableDeclaration["INT"];
        // var sample = iValue?["x"];

        // 2.) Make a Display function
        // Variable["DISPLAY:"] = new Func<object?[], object?>(Display);
        // 3.) Make a Scan function
        // Variable["SCAN:"] = new Func<object?[], object?>(Scan);

        // 4.) Test the expressions

        // 5.) Test if begin and end code cannot be placed anywhere

        // 6.) This declaration is correct: INT y --- apply logic that will accept this declaration

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

        public override object? VisitFunctionCall([NotNull] CodeParser.FunctionCallContext context)
        {
            var name = context.IDENTIFIER().GetText() +":" ;
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

        private object? Display(object?[] args)
        {
            foreach (var arg in args)
            {
                Console.Write(arg);
            }

            return null;
        }

        public override object? VisitDeclaration(CodeParser.DeclarationContext context)
        {   
            string dataType = context.dataType().GetText();

            var varNameArray = context.IDENTIFIER().Select(id => id.GetText()).ToArray();

            // check if value is null or expression is missing so that (INT y) will be accepted
            var value = Visit(context.expression());

            var newVariable = new Variables();
            
            foreach (var name in varNameArray)
            {
                newVariable[name] = value;
                Variable[name] = value;
            }

            VariableDeclaration[dataType] = newVariable;

            return null;
        }

        public override object? VisitAssignment([NotNull] CodeParser.AssignmentContext context)
        {
            var varName = context.IDENTIFIER().GetText();
            var value = Visit(context.expression());

            if (!Variable.ContainsKey(varName))
            {
                throw new Exception($"Variable {varName} is not defined");
            }

            Variable[varName] = value;
            return null;
        }
        public override object? VisitIdentifierExpression(CodeParser.IdentifierExpressionContext context)
        {
            var varName = context.IDENTIFIER().GetText();

            if (!Variable.ContainsKey(varName))
            {
                Console.WriteLine($"Variable {varName} is not defined");
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

        //TODO
        //visit unknown is not recognized by the program    
        public override object? VisitUnknown([NotNull] CodeParser.UnknownContext context)
        {
            var blank_line = context.BLANK_LINE().GetText();

            var colon = context.SEMI_COLON().GetText();

            Console.WriteLine(blank_line);

            if (blank_line != null )
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
