﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\quija\OneDrive\Desktop\3rd Year\Second sem\Intelligent Systems 2\Repositories\interpreter-2\interpreter\Grammar\Code.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace interpreter.Grammar {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class CodeLexer : Lexer {
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, BEGIN_CODE=24, 
		END_CODE=25, INT=26, FLOAT=27, CHAR=28, BOOL=29, INTEGER_VAL=30, FLOAT_VAL=31, 
		STRING_VAL=32, CHARACTER_VAL=33, BOOL_VAL=34, IDENTIFIER=35, COMMENT=36, 
		BLANK_LINE=37, WS=38, BEGIN_IF=39, END_IF=40, WHILE=41, BEGIN_WHILE=42, 
		END_WHILE=43, SCAN=44, LOGICAL_OPERATOR=45;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "T__19", "T__20", "T__21", "T__22", "BEGIN_CODE", "END_CODE", 
		"INT", "FLOAT", "CHAR", "BOOL", "INTEGER_VAL", "FLOAT_VAL", "STRING_VAL", 
		"CHARACTER_VAL", "BOOL_VAL", "IDENTIFIER", "COMMENT", "BLANK_LINE", "WS", 
		"BEGIN_IF", "END_IF", "WHILE", "BEGIN_WHILE", "END_WHILE", "SCAN", "LOGICAL_OPERATOR"
	};


	public CodeLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, "';'", "','", "'='", "'IF'", "'('", "')'", "'ELSE'", "':'", "'NOT'", 
		"'*'", "'/'", "'%'", "'+'", "'-'", "'&'", "'=='", "'<>'", "'>'", "'<'", 
		"'>='", "'<='", "'['", "']'", null, null, "'INT'", "'FLOAT'", "'CHAR'", 
		"'BOOL'", null, null, null, null, null, null, null, null, null, null, 
		null, "'WHILE'", null, null, "'SCAN:'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		"BEGIN_CODE", "END_CODE", "INT", "FLOAT", "CHAR", "BOOL", "INTEGER_VAL", 
		"FLOAT_VAL", "STRING_VAL", "CHARACTER_VAL", "BOOL_VAL", "IDENTIFIER", 
		"COMMENT", "BLANK_LINE", "WS", "BEGIN_IF", "END_IF", "WHILE", "BEGIN_WHILE", 
		"END_WHILE", "SCAN", "LOGICAL_OPERATOR"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}

	[System.Obsolete("Use IRecognizer.Vocabulary instead.")]
	public override string[] TokenNames
	{
		get
		{
			return tokenNames;
		}
	}

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Code.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2/\x150\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t)\x4*\t"+
		"*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x3\x2\x3\x2\x3\x3\x3\x3\x3\x4\x3\x4\x3\x5"+
		"\x3\x5\x3\x5\x3\x6\x3\x6\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t"+
		"\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v\x3\f\x3\f\x3\r\x3\r\x3\xE\x3\xE\x3\xF\x3"+
		"\xF\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x13\x3"+
		"\x13\x3\x14\x3\x14\x3\x15\x3\x15\x3\x15\x3\x16\x3\x16\x3\x16\x3\x17\x3"+
		"\x17\x3\x18\x3\x18\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3"+
		"\x19\x3\x19\x3\x19\x3\x19\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3"+
		"\x1A\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1C\x3"+
		"\x1C\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3"+
		"\x1E\x3\x1E\x3\x1E\x3\x1F\x6\x1F\xBF\n\x1F\r\x1F\xE\x1F\xC0\x3 \x6 \xC4"+
		"\n \r \xE \xC5\x3 \x3 \x6 \xCA\n \r \xE \xCB\x3!\x3!\a!\xD0\n!\f!\xE!"+
		"\xD3\v!\x3!\x3!\x3!\a!\xD8\n!\f!\xE!\xDB\v!\x3!\x5!\xDE\n!\x3\"\x3\"\x3"+
		"\"\x3\"\x3\"\x3\"\x3\"\x3\"\x3\"\x3\"\x3\"\x3\"\x3\"\x5\"\xED\n\"\x3#"+
		"\x3#\x3#\x3#\x3$\x3$\a$\xF5\n$\f$\xE$\xF8\v$\x3%\x3%\a%\xFC\n%\f%\xE%"+
		"\xFF\v%\x3%\x3%\x3&\a&\x104\n&\f&\xE&\x107\v&\x3&\x5&\x10A\n&\x3&\x3&"+
		"\x3\'\x6\'\x10F\n\'\r\'\xE\'\x110\x3\'\x3\'\x3(\x3(\x3(\x3(\x3(\x3(\x3"+
		"(\x3(\x3(\x3)\x3)\x3)\x3)\x3)\x3)\x3)\x3*\x3*\x3*\x3*\x3*\x3*\x3+\x3+"+
		"\x3+\x3+\x3+\x3+\x3+\x3+\x3+\x3+\x3+\x3+\x3,\x3,\x3,\x3,\x3,\x3,\x3,\x3"+
		",\x3,\x3,\x3-\x3-\x3-\x3-\x3-\x3-\x3.\x3.\x3.\x3.\x3.\x3.\x3.\x3.\x5."+
		"\x14F\n.\x2\x2\x2/\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF"+
		"\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10"+
		"\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2\x17-\x2\x18/"+
		"\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39\x2\x1E;\x2\x1F"+
		"=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'M\x2(O\x2)Q\x2*S\x2"+
		"+U\x2,W\x2-Y\x2.[\x2/\x3\x2\r\x3\x2\x32;\x3\x2$$\x3\x2))\x5\x2\f\f\xF"+
		"\xF))\x5\x2\x43\\\x61\x61\x63|\x6\x2\x32;\x43\\\x61\x61\x63|\x4\x2\f\f"+
		"\xF\xF\x4\x2\v\v\"\"\x3\x2\xF\xF\x3\x2\f\f\x5\x2\v\v\xF\xF\"\"\x15D\x2"+
		"\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2"+
		"\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2"+
		"\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2"+
		"\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2"+
		"\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2"+
		"\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2"+
		"\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2"+
		"\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2\x2"+
		"\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2\x2"+
		"\x2K\x3\x2\x2\x2\x2M\x3\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2S\x3"+
		"\x2\x2\x2\x2U\x3\x2\x2\x2\x2W\x3\x2\x2\x2\x2Y\x3\x2\x2\x2\x2[\x3\x2\x2"+
		"\x2\x3]\x3\x2\x2\x2\x5_\x3\x2\x2\x2\a\x61\x3\x2\x2\x2\t\x63\x3\x2\x2\x2"+
		"\v\x66\x3\x2\x2\x2\rh\x3\x2\x2\x2\xFj\x3\x2\x2\x2\x11o\x3\x2\x2\x2\x13"+
		"q\x3\x2\x2\x2\x15u\x3\x2\x2\x2\x17w\x3\x2\x2\x2\x19y\x3\x2\x2\x2\x1B{"+
		"\x3\x2\x2\x2\x1D}\x3\x2\x2\x2\x1F\x7F\x3\x2\x2\x2!\x81\x3\x2\x2\x2#\x84"+
		"\x3\x2\x2\x2%\x87\x3\x2\x2\x2\'\x89\x3\x2\x2\x2)\x8B\x3\x2\x2\x2+\x8E"+
		"\x3\x2\x2\x2-\x91\x3\x2\x2\x2/\x93\x3\x2\x2\x2\x31\x95\x3\x2\x2\x2\x33"+
		"\xA0\x3\x2\x2\x2\x35\xA9\x3\x2\x2\x2\x37\xAD\x3\x2\x2\x2\x39\xB3\x3\x2"+
		"\x2\x2;\xB8\x3\x2\x2\x2=\xBE\x3\x2\x2\x2?\xC3\x3\x2\x2\x2\x41\xDD\x3\x2"+
		"\x2\x2\x43\xEC\x3\x2\x2\x2\x45\xEE\x3\x2\x2\x2G\xF2\x3\x2\x2\x2I\xF9\x3"+
		"\x2\x2\x2K\x105\x3\x2\x2\x2M\x10E\x3\x2\x2\x2O\x114\x3\x2\x2\x2Q\x11D"+
		"\x3\x2\x2\x2S\x124\x3\x2\x2\x2U\x12A\x3\x2\x2\x2W\x136\x3\x2\x2\x2Y\x140"+
		"\x3\x2\x2\x2[\x14E\x3\x2\x2\x2]^\a=\x2\x2^\x4\x3\x2\x2\x2_`\a.\x2\x2`"+
		"\x6\x3\x2\x2\x2\x61\x62\a?\x2\x2\x62\b\x3\x2\x2\x2\x63\x64\aK\x2\x2\x64"+
		"\x65\aH\x2\x2\x65\n\x3\x2\x2\x2\x66g\a*\x2\x2g\f\x3\x2\x2\x2hi\a+\x2\x2"+
		"i\xE\x3\x2\x2\x2jk\aG\x2\x2kl\aN\x2\x2lm\aU\x2\x2mn\aG\x2\x2n\x10\x3\x2"+
		"\x2\x2op\a<\x2\x2p\x12\x3\x2\x2\x2qr\aP\x2\x2rs\aQ\x2\x2st\aV\x2\x2t\x14"+
		"\x3\x2\x2\x2uv\a,\x2\x2v\x16\x3\x2\x2\x2wx\a\x31\x2\x2x\x18\x3\x2\x2\x2"+
		"yz\a\'\x2\x2z\x1A\x3\x2\x2\x2{|\a-\x2\x2|\x1C\x3\x2\x2\x2}~\a/\x2\x2~"+
		"\x1E\x3\x2\x2\x2\x7F\x80\a(\x2\x2\x80 \x3\x2\x2\x2\x81\x82\a?\x2\x2\x82"+
		"\x83\a?\x2\x2\x83\"\x3\x2\x2\x2\x84\x85\a>\x2\x2\x85\x86\a@\x2\x2\x86"+
		"$\x3\x2\x2\x2\x87\x88\a@\x2\x2\x88&\x3\x2\x2\x2\x89\x8A\a>\x2\x2\x8A("+
		"\x3\x2\x2\x2\x8B\x8C\a@\x2\x2\x8C\x8D\a?\x2\x2\x8D*\x3\x2\x2\x2\x8E\x8F"+
		"\a>\x2\x2\x8F\x90\a?\x2\x2\x90,\x3\x2\x2\x2\x91\x92\a]\x2\x2\x92.\x3\x2"+
		"\x2\x2\x93\x94\a_\x2\x2\x94\x30\x3\x2\x2\x2\x95\x96\a\x44\x2\x2\x96\x97"+
		"\aG\x2\x2\x97\x98\aI\x2\x2\x98\x99\aK\x2\x2\x99\x9A\aP\x2\x2\x9A\x9B\x3"+
		"\x2\x2\x2\x9B\x9C\a\x45\x2\x2\x9C\x9D\aQ\x2\x2\x9D\x9E\a\x46\x2\x2\x9E"+
		"\x9F\aG\x2\x2\x9F\x32\x3\x2\x2\x2\xA0\xA1\aG\x2\x2\xA1\xA2\aP\x2\x2\xA2"+
		"\xA3\a\x46\x2\x2\xA3\xA4\x3\x2\x2\x2\xA4\xA5\a\x45\x2\x2\xA5\xA6\aQ\x2"+
		"\x2\xA6\xA7\a\x46\x2\x2\xA7\xA8\aG\x2\x2\xA8\x34\x3\x2\x2\x2\xA9\xAA\a"+
		"K\x2\x2\xAA\xAB\aP\x2\x2\xAB\xAC\aV\x2\x2\xAC\x36\x3\x2\x2\x2\xAD\xAE"+
		"\aH\x2\x2\xAE\xAF\aN\x2\x2\xAF\xB0\aQ\x2\x2\xB0\xB1\a\x43\x2\x2\xB1\xB2"+
		"\aV\x2\x2\xB2\x38\x3\x2\x2\x2\xB3\xB4\a\x45\x2\x2\xB4\xB5\aJ\x2\x2\xB5"+
		"\xB6\a\x43\x2\x2\xB6\xB7\aT\x2\x2\xB7:\x3\x2\x2\x2\xB8\xB9\a\x44\x2\x2"+
		"\xB9\xBA\aQ\x2\x2\xBA\xBB\aQ\x2\x2\xBB\xBC\aN\x2\x2\xBC<\x3\x2\x2\x2\xBD"+
		"\xBF\t\x2\x2\x2\xBE\xBD\x3\x2\x2\x2\xBF\xC0\x3\x2\x2\x2\xC0\xBE\x3\x2"+
		"\x2\x2\xC0\xC1\x3\x2\x2\x2\xC1>\x3\x2\x2\x2\xC2\xC4\t\x2\x2\x2\xC3\xC2"+
		"\x3\x2\x2\x2\xC4\xC5\x3\x2\x2\x2\xC5\xC3\x3\x2\x2\x2\xC5\xC6\x3\x2\x2"+
		"\x2\xC6\xC7\x3\x2\x2\x2\xC7\xC9\a\x30\x2\x2\xC8\xCA\t\x2\x2\x2\xC9\xC8"+
		"\x3\x2\x2\x2\xCA\xCB\x3\x2\x2\x2\xCB\xC9\x3\x2\x2\x2\xCB\xCC\x3\x2\x2"+
		"\x2\xCC@\x3\x2\x2\x2\xCD\xD1\a$\x2\x2\xCE\xD0\n\x3\x2\x2\xCF\xCE\x3\x2"+
		"\x2\x2\xD0\xD3\x3\x2\x2\x2\xD1\xCF\x3\x2\x2\x2\xD1\xD2\x3\x2\x2\x2\xD2"+
		"\xD4\x3\x2\x2\x2\xD3\xD1\x3\x2\x2\x2\xD4\xDE\a$\x2\x2\xD5\xD9\a)\x2\x2"+
		"\xD6\xD8\n\x4\x2\x2\xD7\xD6\x3\x2\x2\x2\xD8\xDB\x3\x2\x2\x2\xD9\xD7\x3"+
		"\x2\x2\x2\xD9\xDA\x3\x2\x2\x2\xDA\xDC\x3\x2\x2\x2\xDB\xD9\x3\x2\x2\x2"+
		"\xDC\xDE\a)\x2\x2\xDD\xCD\x3\x2\x2\x2\xDD\xD5\x3\x2\x2\x2\xDE\x42\x3\x2"+
		"\x2\x2\xDF\xE0\a$\x2\x2\xE0\xE1\aV\x2\x2\xE1\xE2\aT\x2\x2\xE2\xE3\aW\x2"+
		"\x2\xE3\xE4\aG\x2\x2\xE4\xED\a$\x2\x2\xE5\xE6\a$\x2\x2\xE6\xE7\aH\x2\x2"+
		"\xE7\xE8\a\x43\x2\x2\xE8\xE9\aN\x2\x2\xE9\xEA\aU\x2\x2\xEA\xEB\aG\x2\x2"+
		"\xEB\xED\a$\x2\x2\xEC\xDF\x3\x2\x2\x2\xEC\xE5\x3\x2\x2\x2\xED\x44\x3\x2"+
		"\x2\x2\xEE\xEF\a)\x2\x2\xEF\xF0\n\x5\x2\x2\xF0\xF1\a)\x2\x2\xF1\x46\x3"+
		"\x2\x2\x2\xF2\xF6\t\x6\x2\x2\xF3\xF5\t\a\x2\x2\xF4\xF3\x3\x2\x2\x2\xF5"+
		"\xF8\x3\x2\x2\x2\xF6\xF4\x3\x2\x2\x2\xF6\xF7\x3\x2\x2\x2\xF7H\x3\x2\x2"+
		"\x2\xF8\xF6\x3\x2\x2\x2\xF9\xFD\a%\x2\x2\xFA\xFC\n\b\x2\x2\xFB\xFA\x3"+
		"\x2\x2\x2\xFC\xFF\x3\x2\x2\x2\xFD\xFB\x3\x2\x2\x2\xFD\xFE\x3\x2\x2\x2"+
		"\xFE\x100\x3\x2\x2\x2\xFF\xFD\x3\x2\x2\x2\x100\x101\b%\x2\x2\x101J\x3"+
		"\x2\x2\x2\x102\x104\t\t\x2\x2\x103\x102\x3\x2\x2\x2\x104\x107\x3\x2\x2"+
		"\x2\x105\x103\x3\x2\x2\x2\x105\x106\x3\x2\x2\x2\x106\x109\x3\x2\x2\x2"+
		"\x107\x105\x3\x2\x2\x2\x108\x10A\t\n\x2\x2\x109\x108\x3\x2\x2\x2\x109"+
		"\x10A\x3\x2\x2\x2\x10A\x10B\x3\x2\x2\x2\x10B\x10C\t\v\x2\x2\x10CL\x3\x2"+
		"\x2\x2\x10D\x10F\t\f\x2\x2\x10E\x10D\x3\x2\x2\x2\x10F\x110\x3\x2\x2\x2"+
		"\x110\x10E\x3\x2\x2\x2\x110\x111\x3\x2\x2\x2\x111\x112\x3\x2\x2\x2\x112"+
		"\x113\b\'\x2\x2\x113N\x3\x2\x2\x2\x114\x115\a\x44\x2\x2\x115\x116\aG\x2"+
		"\x2\x116\x117\aI\x2\x2\x117\x118\aK\x2\x2\x118\x119\aP\x2\x2\x119\x11A"+
		"\x3\x2\x2\x2\x11A\x11B\aK\x2\x2\x11B\x11C\aH\x2\x2\x11CP\x3\x2\x2\x2\x11D"+
		"\x11E\aG\x2\x2\x11E\x11F\aP\x2\x2\x11F\x120\a\x46\x2\x2\x120\x121\x3\x2"+
		"\x2\x2\x121\x122\aK\x2\x2\x122\x123\aH\x2\x2\x123R\x3\x2\x2\x2\x124\x125"+
		"\aY\x2\x2\x125\x126\aJ\x2\x2\x126\x127\aK\x2\x2\x127\x128\aN\x2\x2\x128"+
		"\x129\aG\x2\x2\x129T\x3\x2\x2\x2\x12A\x12B\a\x44\x2\x2\x12B\x12C\aG\x2"+
		"\x2\x12C\x12D\aI\x2\x2\x12D\x12E\aK\x2\x2\x12E\x12F\aP\x2\x2\x12F\x130"+
		"\x3\x2\x2\x2\x130\x131\aY\x2\x2\x131\x132\aJ\x2\x2\x132\x133\aK\x2\x2"+
		"\x133\x134\aN\x2\x2\x134\x135\aG\x2\x2\x135V\x3\x2\x2\x2\x136\x137\aG"+
		"\x2\x2\x137\x138\aP\x2\x2\x138\x139\a\x46\x2\x2\x139\x13A\x3\x2\x2\x2"+
		"\x13A\x13B\aY\x2\x2\x13B\x13C\aJ\x2\x2\x13C\x13D\aK\x2\x2\x13D\x13E\a"+
		"N\x2\x2\x13E\x13F\aG\x2\x2\x13FX\x3\x2\x2\x2\x140\x141\aU\x2\x2\x141\x142"+
		"\a\x45\x2\x2\x142\x143\a\x43\x2\x2\x143\x144\aP\x2\x2\x144\x145\a<\x2"+
		"\x2\x145Z\x3\x2\x2\x2\x146\x147\a\x43\x2\x2\x147\x148\aP\x2\x2\x148\x14F"+
		"\a\x46\x2\x2\x149\x14A\aQ\x2\x2\x14A\x14F\aT\x2\x2\x14B\x14C\aP\x2\x2"+
		"\x14C\x14D\aQ\x2\x2\x14D\x14F\aV\x2\x2\x14E\x146\x3\x2\x2\x2\x14E\x149"+
		"\x3\x2\x2\x2\x14E\x14B\x3\x2\x2\x2\x14F\\\x3\x2\x2\x2\x10\x2\xC0\xC5\xCB"+
		"\xD1\xD9\xDD\xEC\xF6\xFD\x105\x109\x110\x14E\x3\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace interpreter.Grammar
