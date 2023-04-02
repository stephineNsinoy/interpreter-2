using Antlr4.Runtime;
using interpreter;
using interpreter.Grammar;

var fileName = "Grammar\\test.txt";

var fileContents = File.ReadAllText(fileName);

var inputStream = new AntlrInputStream(fileContents);

var codeLexer = new CodeLexer(inputStream);
var commonTokenStream = new CommonTokenStream(codeLexer);
var codeParser = new CodeParser(commonTokenStream);
codeParser.RemoveErrorListeners();
codeParser.AddErrorListener(new SyntaxErrorEvaluator());
var visitor = new CodeVisitor();
var codeContext = codeParser.program();
visitor.Visit(codeContext);