//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /home/skytgtr/ANTLR/commands/commands.g by ANTLR 4.7.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public partial class commandsLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		WS=1, ENDLINE=2, USER_NAME=3, CMD=4;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"ID", "WS", "ENDLINE", "USER_NAME", "CMD"
	};


	public commandsLexer(ICharStream input)
	: this(input, System.Console.Out, System.Console.Error) { }

	public commandsLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
	};
	private static readonly string[] _SymbolicNames = {
		null, "WS", "ENDLINE", "USER_NAME", "CMD"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "commands.g"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static commandsLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', '\x6', '\xC8', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x3', '\x2', '\x3', '\x2', '\x3', '\x3', 
		'\x6', '\x3', '\x11', '\n', '\x3', '\r', '\x3', '\xE', '\x3', '\x12', 
		'\x3', '\x4', '\x5', '\x4', '\x16', '\n', '\x4', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x5', '\x3', '\x5', '\a', '\x5', '\x1C', '\n', '\x5', '\f', 
		'\x5', '\xE', '\x5', '\x1F', '\v', '\x5', '\x3', '\x5', '\x3', '\x5', 
		'\a', '\x5', '#', '\n', '\x5', '\f', '\x5', '\xE', '\x5', '&', '\v', '\x5', 
		'\x3', '\x5', '\x3', '\x5', '\x3', '\x5', '\a', '\x5', '+', '\n', '\x5', 
		'\f', '\x5', '\xE', '\x5', '.', '\v', '\x5', '\x3', '\x5', '\x5', '\x5', 
		'\x31', '\n', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x5', '\x6', '\x39', '\n', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x5', '\x6', 'Q', '\n', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x5', '\x6', '_', '\n', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x5', '\x6', 'o', '\n', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x5', '\x6', '\x89', '\n', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\x6', '\x5', '\x6', '\x91', '\n', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x5', '\x6', '\x9B', '\n', '\x6', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x5', '\x6', '\xA5', '\n', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x5', '\x6', '\xAF', '\n', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x5', '\x6', '\xB9', 
		'\n', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x5', '\x6', '\xC5', '\n', '\x6', '\x5', '\x6', '\xC7', '\n', 
		'\x6', '\x2', '\x2', '\a', '\x3', '\x2', '\x5', '\x3', '\a', '\x4', '\t', 
		'\x5', '\v', '\x6', '\x3', '\x2', '\x4', '\a', '\x2', '/', '\x30', '\x32', 
		';', '\x43', '\\', '\x61', '\x61', '\x63', '|', '\x4', '\x2', '\v', '\v', 
		'\"', '\"', '\x2', '\xE1', '\x2', '\x5', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\a', '\x3', '\x2', '\x2', '\x2', '\x2', '\t', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\v', '\x3', '\x2', '\x2', '\x2', '\x3', '\r', '\x3', '\x2', '\x2', 
		'\x2', '\x5', '\x10', '\x3', '\x2', '\x2', '\x2', '\a', '\x15', '\x3', 
		'\x2', '\x2', '\x2', '\t', '\x30', '\x3', '\x2', '\x2', '\x2', '\v', '\xC6', 
		'\x3', '\x2', '\x2', '\x2', '\r', '\xE', '\t', '\x2', '\x2', '\x2', '\xE', 
		'\x4', '\x3', '\x2', '\x2', '\x2', '\xF', '\x11', '\t', '\x3', '\x2', 
		'\x2', '\x10', '\xF', '\x3', '\x2', '\x2', '\x2', '\x11', '\x12', '\x3', 
		'\x2', '\x2', '\x2', '\x12', '\x10', '\x3', '\x2', '\x2', '\x2', '\x12', 
		'\x13', '\x3', '\x2', '\x2', '\x2', '\x13', '\x6', '\x3', '\x2', '\x2', 
		'\x2', '\x14', '\x16', '\a', '\xF', '\x2', '\x2', '\x15', '\x14', '\x3', 
		'\x2', '\x2', '\x2', '\x15', '\x16', '\x3', '\x2', '\x2', '\x2', '\x16', 
		'\x17', '\x3', '\x2', '\x2', '\x2', '\x17', '\x18', '\a', '\f', '\x2', 
		'\x2', '\x18', '\b', '\x3', '\x2', '\x2', '\x2', '\x19', '\x1D', '\a', 
		')', '\x2', '\x2', '\x1A', '\x1C', '\x5', '\x3', '\x2', '\x2', '\x1B', 
		'\x1A', '\x3', '\x2', '\x2', '\x2', '\x1C', '\x1F', '\x3', '\x2', '\x2', 
		'\x2', '\x1D', '\x1B', '\x3', '\x2', '\x2', '\x2', '\x1D', '\x1E', '\x3', 
		'\x2', '\x2', '\x2', '\x1E', ' ', '\x3', '\x2', '\x2', '\x2', '\x1F', 
		'\x1D', '\x3', '\x2', '\x2', '\x2', ' ', '$', '\a', '\x42', '\x2', '\x2', 
		'!', '#', '\x5', '\x3', '\x2', '\x2', '\"', '!', '\x3', '\x2', '\x2', 
		'\x2', '#', '&', '\x3', '\x2', '\x2', '\x2', '$', '\"', '\x3', '\x2', 
		'\x2', '\x2', '$', '%', '\x3', '\x2', '\x2', '\x2', '%', '\'', '\x3', 
		'\x2', '\x2', '\x2', '&', '$', '\x3', '\x2', '\x2', '\x2', '\'', '\x31', 
		'\a', ')', '\x2', '\x2', '(', ',', '\a', ')', '\x2', '\x2', ')', '+', 
		'\x5', '\x3', '\x2', '\x2', '*', ')', '\x3', '\x2', '\x2', '\x2', '+', 
		'.', '\x3', '\x2', '\x2', '\x2', ',', '*', '\x3', '\x2', '\x2', '\x2', 
		',', '-', '\x3', '\x2', '\x2', '\x2', '-', '/', '\x3', '\x2', '\x2', '\x2', 
		'.', ',', '\x3', '\x2', '\x2', '\x2', '/', '\x31', '\a', ')', '\x2', '\x2', 
		'\x30', '\x19', '\x3', '\x2', '\x2', '\x2', '\x30', '(', '\x3', '\x2', 
		'\x2', '\x2', '\x31', '\n', '\x3', '\x2', '\x2', '\x2', '\x32', '\x33', 
		'\a', '\x43', '\x2', '\x2', '\x33', '\x34', '\a', '\x46', '\x2', '\x2', 
		'\x34', '\x39', '\a', '\x46', '\x2', '\x2', '\x35', '\x36', '\a', '\x63', 
		'\x2', '\x2', '\x36', '\x37', '\a', '\x66', '\x2', '\x2', '\x37', '\x39', 
		'\a', '\x66', '\x2', '\x2', '\x38', '\x32', '\x3', '\x2', '\x2', '\x2', 
		'\x38', '\x35', '\x3', '\x2', '\x2', '\x2', '\x39', '\xC7', '\x3', '\x2', 
		'\x2', '\x2', ':', ';', '\a', 'u', '\x2', '\x2', ';', '<', '\a', 'g', 
		'\x2', '\x2', '<', '=', '\a', 'v', '\x2', '\x2', '=', '>', '\a', 'r', 
		'\x2', '\x2', '>', '?', '\a', '\x63', '\x2', '\x2', '?', '@', '\a', 'u', 
		'\x2', '\x2', '@', '\x41', '\a', 'u', '\x2', '\x2', '\x41', '\x42', '\a', 
		'y', '\x2', '\x2', '\x42', '\x43', '\a', 'q', '\x2', '\x2', '\x43', '\x44', 
		'\a', 't', '\x2', '\x2', '\x44', 'Q', '\a', '\x66', '\x2', '\x2', '\x45', 
		'\x46', '\a', 'U', '\x2', '\x2', '\x46', 'G', '\a', 'G', '\x2', '\x2', 
		'G', 'H', '\a', 'V', '\x2', '\x2', 'H', 'I', '\a', 'R', '\x2', '\x2', 
		'I', 'J', '\a', '\x43', '\x2', '\x2', 'J', 'K', '\a', 'U', '\x2', '\x2', 
		'K', 'L', '\a', 'U', '\x2', '\x2', 'L', 'M', '\a', 'Y', '\x2', '\x2', 
		'M', 'N', '\a', 'Q', '\x2', '\x2', 'N', 'O', '\a', 'T', '\x2', '\x2', 
		'O', 'Q', '\a', '\x46', '\x2', '\x2', 'P', ':', '\x3', '\x2', '\x2', '\x2', 
		'P', '\x45', '\x3', '\x2', '\x2', '\x2', 'Q', '\xC7', '\x3', '\x2', '\x2', 
		'\x2', 'R', 'S', '\a', 'G', '\x2', '\x2', 'S', 'T', '\a', 'P', '\x2', 
		'\x2', 'T', 'U', '\a', '\x43', '\x2', '\x2', 'U', 'V', '\a', '\x44', '\x2', 
		'\x2', 'V', 'W', '\a', 'N', '\x2', '\x2', 'W', '_', '\a', 'G', '\x2', 
		'\x2', 'X', 'Y', '\a', 'g', '\x2', '\x2', 'Y', 'Z', '\a', 'p', '\x2', 
		'\x2', 'Z', '[', '\a', '\x63', '\x2', '\x2', '[', '\\', '\a', '\x64', 
		'\x2', '\x2', '\\', ']', '\a', 'n', '\x2', '\x2', ']', '_', '\a', 'g', 
		'\x2', '\x2', '^', 'R', '\x3', '\x2', '\x2', '\x2', '^', 'X', '\x3', '\x2', 
		'\x2', '\x2', '_', '\xC7', '\x3', '\x2', '\x2', '\x2', '`', '\x61', '\a', 
		'\x46', '\x2', '\x2', '\x61', '\x62', '\a', 'K', '\x2', '\x2', '\x62', 
		'\x63', '\a', 'U', '\x2', '\x2', '\x63', '\x64', '\a', '\x43', '\x2', 
		'\x2', '\x64', '\x65', '\a', '\x44', '\x2', '\x2', '\x65', '\x66', '\a', 
		'N', '\x2', '\x2', '\x66', 'o', '\a', 'G', '\x2', '\x2', 'g', 'h', '\a', 
		'\x66', '\x2', '\x2', 'h', 'i', '\a', 'k', '\x2', '\x2', 'i', 'j', '\a', 
		'u', '\x2', '\x2', 'j', 'k', '\a', '\x63', '\x2', '\x2', 'k', 'l', '\a', 
		'\x64', '\x2', '\x2', 'l', 'm', '\a', 'n', '\x2', '\x2', 'm', 'o', '\a', 
		'g', '\x2', '\x2', 'n', '`', '\x3', '\x2', '\x2', '\x2', 'n', 'g', '\x3', 
		'\x2', '\x2', '\x2', 'o', '\xC7', '\x3', '\x2', '\x2', '\x2', 'p', 'q', 
		'\a', 'V', '\x2', '\x2', 'q', 'r', '\a', 'G', '\x2', '\x2', 'r', 's', 
		'\a', 'U', '\x2', '\x2', 's', 't', '\a', 'V', '\x2', '\x2', 't', 'u', 
		'\a', 'R', '\x2', '\x2', 'u', 'v', '\a', '\x43', '\x2', '\x2', 'v', 'w', 
		'\a', 'U', '\x2', '\x2', 'w', 'x', '\a', 'U', '\x2', '\x2', 'x', 'y', 
		'\a', 'Y', '\x2', '\x2', 'y', 'z', '\a', 'Q', '\x2', '\x2', 'z', '{', 
		'\a', 'T', '\x2', '\x2', '{', '\x89', '\a', '\x46', '\x2', '\x2', '|', 
		'}', '\a', 'v', '\x2', '\x2', '}', '~', '\a', 'g', '\x2', '\x2', '~', 
		'\x7F', '\a', 'u', '\x2', '\x2', '\x7F', '\x80', '\a', 'v', '\x2', '\x2', 
		'\x80', '\x81', '\a', 'r', '\x2', '\x2', '\x81', '\x82', '\a', '\x63', 
		'\x2', '\x2', '\x82', '\x83', '\a', 'u', '\x2', '\x2', '\x83', '\x84', 
		'\a', 'u', '\x2', '\x2', '\x84', '\x85', '\a', 'y', '\x2', '\x2', '\x85', 
		'\x86', '\a', 'q', '\x2', '\x2', '\x86', '\x87', '\a', 't', '\x2', '\x2', 
		'\x87', '\x89', '\a', '\x66', '\x2', '\x2', '\x88', 'p', '\x3', '\x2', 
		'\x2', '\x2', '\x88', '|', '\x3', '\x2', '\x2', '\x2', '\x89', '\xC7', 
		'\x3', '\x2', '\x2', '\x2', '\x8A', '\x8B', '\a', '\x66', '\x2', '\x2', 
		'\x8B', '\x8C', '\a', 'g', '\x2', '\x2', '\x8C', '\x91', '\a', 'n', '\x2', 
		'\x2', '\x8D', '\x8E', '\a', '\x46', '\x2', '\x2', '\x8E', '\x8F', '\a', 
		'G', '\x2', '\x2', '\x8F', '\x91', '\a', 'N', '\x2', '\x2', '\x90', '\x8A', 
		'\x3', '\x2', '\x2', '\x2', '\x90', '\x8D', '\x3', '\x2', '\x2', '\x2', 
		'\x91', '\xC7', '\x3', '\x2', '\x2', '\x2', '\x92', '\x93', '\a', 'n', 
		'\x2', '\x2', '\x93', '\x94', '\a', 'k', '\x2', '\x2', '\x94', '\x95', 
		'\a', 'u', '\x2', '\x2', '\x95', '\x9B', '\a', 'v', '\x2', '\x2', '\x96', 
		'\x97', '\a', 'N', '\x2', '\x2', '\x97', '\x98', '\a', 'K', '\x2', '\x2', 
		'\x98', '\x99', '\a', 'U', '\x2', '\x2', '\x99', '\x9B', '\a', 'V', '\x2', 
		'\x2', '\x9A', '\x92', '\x3', '\x2', '\x2', '\x2', '\x9A', '\x96', '\x3', 
		'\x2', '\x2', '\x2', '\x9B', '\xC7', '\x3', '\x2', '\x2', '\x2', '\x9C', 
		'\x9D', '\a', 'j', '\x2', '\x2', '\x9D', '\x9E', '\a', 'g', '\x2', '\x2', 
		'\x9E', '\x9F', '\a', 'n', '\x2', '\x2', '\x9F', '\xA5', '\a', 'r', '\x2', 
		'\x2', '\xA0', '\xA1', '\a', 'J', '\x2', '\x2', '\xA1', '\xA2', '\a', 
		'G', '\x2', '\x2', '\xA2', '\xA3', '\a', 'N', '\x2', '\x2', '\xA3', '\xA5', 
		'\a', 'R', '\x2', '\x2', '\xA4', '\x9C', '\x3', '\x2', '\x2', '\x2', '\xA4', 
		'\xA0', '\x3', '\x2', '\x2', '\x2', '\xA5', '\xC7', '\x3', '\x2', '\x2', 
		'\x2', '\xA6', '\xA7', '\a', 'g', '\x2', '\x2', '\xA7', '\xA8', '\a', 
		'z', '\x2', '\x2', '\xA8', '\xA9', '\a', 'k', '\x2', '\x2', '\xA9', '\xAF', 
		'\a', 'v', '\x2', '\x2', '\xAA', '\xAB', '\a', 'G', '\x2', '\x2', '\xAB', 
		'\xAC', '\a', 'Z', '\x2', '\x2', '\xAC', '\xAD', '\a', 'K', '\x2', '\x2', 
		'\xAD', '\xAF', '\a', 'V', '\x2', '\x2', '\xAE', '\xA6', '\x3', '\x2', 
		'\x2', '\x2', '\xAE', '\xAA', '\x3', '\x2', '\x2', '\x2', '\xAF', '\xC7', 
		'\x3', '\x2', '\x2', '\x2', '\xB0', '\xB1', '\a', 's', '\x2', '\x2', '\xB1', 
		'\xB2', '\a', 'w', '\x2', '\x2', '\xB2', '\xB3', '\a', 'k', '\x2', '\x2', 
		'\xB3', '\xB9', '\a', 'v', '\x2', '\x2', '\xB4', '\xB5', '\a', 'S', '\x2', 
		'\x2', '\xB5', '\xB6', '\a', 'W', '\x2', '\x2', '\xB6', '\xB7', '\a', 
		'K', '\x2', '\x2', '\xB7', '\xB9', '\a', 'V', '\x2', '\x2', '\xB8', '\xB0', 
		'\x3', '\x2', '\x2', '\x2', '\xB8', '\xB4', '\x3', '\x2', '\x2', '\x2', 
		'\xB9', '\xC7', '\x3', '\x2', '\x2', '\x2', '\xBA', '\xBB', '\a', '\x65', 
		'\x2', '\x2', '\xBB', '\xBC', '\a', 'n', '\x2', '\x2', '\xBC', '\xBD', 
		'\a', 'q', '\x2', '\x2', '\xBD', '\xBE', '\a', 'u', '\x2', '\x2', '\xBE', 
		'\xC5', '\a', 'g', '\x2', '\x2', '\xBF', '\xC0', '\a', '\x45', '\x2', 
		'\x2', '\xC0', '\xC1', '\a', 'N', '\x2', '\x2', '\xC1', '\xC2', '\a', 
		'Q', '\x2', '\x2', '\xC2', '\xC3', '\a', 'U', '\x2', '\x2', '\xC3', '\xC5', 
		'\a', 'G', '\x2', '\x2', '\xC4', '\xBA', '\x3', '\x2', '\x2', '\x2', '\xC4', 
		'\xBF', '\x3', '\x2', '\x2', '\x2', '\xC5', '\xC7', '\x3', '\x2', '\x2', 
		'\x2', '\xC6', '\x38', '\x3', '\x2', '\x2', '\x2', '\xC6', 'P', '\x3', 
		'\x2', '\x2', '\x2', '\xC6', '^', '\x3', '\x2', '\x2', '\x2', '\xC6', 
		'n', '\x3', '\x2', '\x2', '\x2', '\xC6', '\x88', '\x3', '\x2', '\x2', 
		'\x2', '\xC6', '\x90', '\x3', '\x2', '\x2', '\x2', '\xC6', '\x9A', '\x3', 
		'\x2', '\x2', '\x2', '\xC6', '\xA4', '\x3', '\x2', '\x2', '\x2', '\xC6', 
		'\xAE', '\x3', '\x2', '\x2', '\x2', '\xC6', '\xB8', '\x3', '\x2', '\x2', 
		'\x2', '\xC6', '\xC4', '\x3', '\x2', '\x2', '\x2', '\xC7', '\f', '\x3', 
		'\x2', '\x2', '\x2', '\x15', '\x2', '\x12', '\x15', '\x1D', '$', ',', 
		'\x30', '\x38', 'P', '^', 'n', '\x88', '\x90', '\x9A', '\xA4', '\xAE', 
		'\xB8', '\xC4', '\xC6', '\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
