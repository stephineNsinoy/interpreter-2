grammar Code;

program: lineBlock EOF ;

BEGIN_CODE: NEWLINE? 'BEGIN CODE' NEWLINE ;
END_CODE: 'END CODE' NEWLINE?;

// GOODS
BEGIN_IF: 'BEGIN IF' ;
END_IF: 'END IF' ;

lineBlock: BEGIN_CODE declaration* line* END_CODE ;

line: (statement | ifBlock | whileBlock) NEWLINE;

statement: assignment | functionCall;

declaration: dataType IDENTIFIER ('=' expression)? (',' IDENTIFIER ('=' expression)?)* NEWLINE ;
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


ifBlock: 'IF' '('expression')' NEWLINE BEGIN_IF NEWLINE line* END_IF elseIfBlock? elseBlock? ;
elseIfBlock: 'ELSE IF' '('expression')' NEWLINE BEGIN_IF NEWLINE line* NEWLINE END_IF elseIfBlock? elseBlock ;
elseBlock: 'ELSE' NEWLINE BEGIN_IF NEWLINE line* END_IF ;

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

COMMENT: '#' ~[\r\n]* NEWLINE? -> skip ;
NEWLINE: [\r?\n]+ ;
WS: [ \t\r]+ -> skip ;