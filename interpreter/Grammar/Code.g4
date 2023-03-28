grammar Code;

program: lineBlock EOF ;

BEGIN_CODE: NEWLINE? 'BEGIN CODE' ;
END_CODE:  NEWLINE 'END CODE' NEWLINE?;

lineBlock: BEGIN_CODE declaration* line* END_CODE ;

line: NEWLINE? (statement | ifBlock | whileBlock);

statement: assignment | functionCall;

declaration: NEWLINE? dataType IDENTIFIER ('=' expression)? (',' IDENTIFIER ('=' expression)?)* ;
assignment: IDENTIFIER ('=' IDENTIFIER)* '=' expression ;

// GOODS
dataType: INT | FLOAT | BOOL | CHAR | STRING;
INT: 'INT' ;
FLOAT: 'FLOAT';
CHAR: 'CHAR';
BOOL: 'BOOL';
STRING: 'STRING' ;

constant: INTEGER_VAL | FLOAT_VAL | CHAR_VAL | BOOL_VAL | STRING_VAL ;
INTEGER_VAL: [0-9]+ ;
FLOAT_VAL: [0-9]+ '.' [0-9]+ ;
STRING_VAL:  '"' ( ~('"' | '\\') | '\\' . )* '"';
BOOL_VAL: '"TRUE"' | '"FALSE"' ;
CHAR_VAL: ('\'' ~[\r\n\'] '\'') | '[' .? ']' ; 

// GOODS
BEGIN_IF: 'BEGIN IF' ;
END_IF: 'END IF' ;

ifBlock: 'IF' '('expression')' BEGIN_IF line* END_IF elseIfBlock? ;
elseIfBlock: 'ELSE' (BEGIN_IF line* END_IF) | ifBlock ;

// GOODS
WHILE: 'WHILE' ;
BEGIN_WHILE: 'BEGIN WHILE' ;
END_WHILE: 'END WHILE' ;
whileBlock: WHILE '(' expression ')' NEWLINE BEGIN_WHILE NEWLINE line* END_WHILE NEWLINE;

functionCall
    : DISPLAY (expression (',' expression)*)? 
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

// GOODS
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]* ;
NEXTLINE: '$' ;
COMMENT: NEWLINE? '#' ~[\r?\n]*-> channel(HIDDEN);
NEWLINE: ('\r'? '\n')+;
WS: [\t]+ -> skip ;