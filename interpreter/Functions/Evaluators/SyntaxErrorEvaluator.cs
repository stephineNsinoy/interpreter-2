using Antlr4.Runtime;

internal class SyntaxErrorEvaluator : BaseErrorListener
{
    public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        Console.WriteLine($"SYNTAX ERROR: {msg} at line {line} and position {charPositionInLine}");
        Environment.Exit(400);
    }
}