using Antlr4.Runtime;
using interpreter;
using interpreter.;
using interpreter.Grammar;

var fileName = "content\\test.txt";

var fileContents = File.ReadAllText(fileName);

var inputStream = new AntlrInputStream(fileContents);

var simpleLexer = new CodeLexer(inputStream);
var commonTokenStream = new CommonTokenStream(simpleLexer);
var simpleParser = new CodeParser(commonTokenStream);
var simpleContext = simpleParser.program();
var visitor = new CodeVisitor();
visitor.Visit(simpleContext);