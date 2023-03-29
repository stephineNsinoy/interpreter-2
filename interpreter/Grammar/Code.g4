grammar Code;

program: lineBlock EOF ;

BEGIN_CODE: NEWLINE? 'BEGIN CODE' ;
END_CODE:  NEWLINE 'END CODE' NEWLINE?;

lineBlock: BEGIN_CODE declaration* line* END_CODE ;

line: NEWLINE (statement | ifBlock | whileBlock | switchCaseBlock); 

statement: assignment | functionCall;

declaration: NEWLINE dataType IDENTIFIER ('=' expression)? (',' IDENTIFIER ('=' expression)?)* ;
assignment: IDENTIFIER ('=' IDENTIFIER)* '=' expression;

dataType: INT | FLOAT | BOOL | CHAR | STRING;
INT: 'INT' ;
FLOAT: 'FLOAT';
CHAR: 'CHAR';
BOOL: 'BOOL';
STRING: 'STRING' ;

constant: INTEGER_VAL | FLOAT_VAL | CHAR_VAL | BOOL_VAL | STRING_VAL ;
INTEGER_VAL: [0-9]+ ;
FLOAT_VAL: [0-9]+ '.' [0-9]+ ;
BOOL_VAL: '"TRUE"' | '"FALSE"' ;
STRING_VAL:  '"' ( ~('"' | '\\') | '\\' . )* '"';
CHAR_VAL: ('\'' ~[\r\n\'] '\'') | '[' .? ']' ; 

BEGIN_IF: 'BEGIN IF' ;
END_IF: 'END IF' ;

ifBlock: 'IF' '('expression')' BEGIN_IF line* END_IF elseIfBlock? ;
elseIfBlock: 'ELSE' (BEGIN_IF line* END_IF) | ifBlock ;

WHILE: 'WHILE' ;
BEGIN_WHILE: NEWLINE 'BEGIN WHILE' ;
END_WHILE: NEWLINE 'END WHILE' ;
whileBlock: WHILE '(' expression ')' BEGIN_WHILE line* END_WHILE ;

BEGIN_SWITCH: NEWLINE 'BEGIN SWITCH';
END_SWITCH: NEWLINE 'END SWITCH';
switchCaseBlock: 'SWITCH' '(' expression ')' BEGIN_SWITCH caseBlock* defaultBlock? END_SWITCH;

BEGIN_CASE: NEWLINE 'BEGIN CASE';
END_CASE: NEWLINE 'END CASE';
caseBlock: NEWLINE 'CASE' expression ':' BEGIN_CASE line* END_CASE;

BEGIN_DEFAULT: NEWLINE 'BEGIN DEFAULT';
END_DEFAULT: NEWLINE 'END DEFAULT';
defaultBlock: NEWLINE 'DEFAULT:' BEGIN_DEFAULT line* END_DEFAULT;

functionCall
    : DISPLAY expression
    | SCAN IDENTIFIER (',' IDENTIFIER)* ;

DISPLAY: 'DISPLAY:' ;
SCAN: 'SCAN:' ;

expression
    : constant                          #constantExpression
    | IDENTIFIER                        #identifierExpression
    | functionCall                      #functionCallExpression
    | '(' expression ')'                #parenthesizedExpression
    | 'NOT' expression                  #notExpression
    | unary expression                  #unaryExpression
    | expression multOp expression      #multiplicativeExpression
    | expression addOp expression       #additiveExpression
    | expression compareOp expression   #comparativeExpression
    | expression logicOp expression     #booleanExpression
    | expression concat expression      #concatExpression
    | NEXTLINE                          #nextLineExpression
    ;

multOp: '*' | '/' | '%' ;
addOp: '+' | '-' ;
compareOp: '==' | '<>' | '>' | '<' | '>=' | '<='  ;
unary: '+' | '-' ;
concat: '&' ;
logicOp: LOGICAL_OPERATOR ;
LOGICAL_OPERATOR: 'AND' | 'OR' ;

IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]* ;
NEXTLINE: '$' ;
COMMENT: NEWLINE? '#' ~[\r?\n]* -> channel(HIDDEN);
NEWLINE: ('\r'? '\n')+;
WS: [\t]+ -> skip ;