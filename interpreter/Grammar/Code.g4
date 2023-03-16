grammar Code;

program: lineBlock EOF ;

BEGIN_CODE: 'BEGIN CODE' ;
END_CODE: 'END CODE' ;

lineBlock: BEGIN_CODE declaration* line* END_CODE ;

line: statement | ifBlock | whileBlock;

statement: assignment | functionCall;

declaration: dataType IDENTIFIER ('=' expression)? (',' IDENTIFIER ('=' expression)?)* ;
// declaration: dataType IDENTIFIER (',' IDENTIFIER)* ('=' expression)? ;
assignment: IDENTIFIER ('=' IDENTIFIER)* '=' expression ;

// GOODS
dataType: INT | FLOAT | BOOL | CHAR | STRING;
INT: 'INT' ;
FLOAT: 'FLOAT';
CHAR: 'CHAR';
BOOL: 'BOOL';
STRING: 'STRING' ;

// block: (BEGIN_CODE | BEGIN_IF | BEGIN_WHILE);

constant: INTEGER_VAL | FLOAT_VAL | CHAR_VAL | BOOL_VAL | STRING_VAL ;
INTEGER_VAL: ('-' | '+')? [0-9]+ ;
FLOAT_VAL: ('-' | '+')? [0-9]+ '.' [0-9]+ ;
// STRING_VAL: '"' ( ~('"' | '\\') | '\\' . )* '"' ~('"' ('TRUE' | 'FALSE') '"')?;
STRING_VAL:  '"' ( ~('"' | '\\') | '\\' . )* '"';
BOOL_VAL: '"TRUE"' | '"FALSE"' ;
CHAR_VAL: '\'' ~[\r\n\'] '\'' ; 

// GOODS
BEGIN_IF: 'BEGIN IF' ;
END_IF: 'END IF' ;

ifBlock: 'IF' '('expression')' BEGIN_IF line* END_IF elseIfBlock? ;
elseIfBlock: 'ELSE' (BEGIN_IF line* END_IF) | ifBlock ;

// GOODS
WHILE: 'WHILE' ;
BEGIN_WHILE: 'BEGIN WHILE' ;
END_WHILE: 'END WHILE' ;
whileBlock: WHILE '(' expression ')' BEGIN_WHILE line* END_WHILE ;

// for DISPLAY: and SCAN:
functionCall: FUNCTIONS ':' (expression (',' expression)*)? ;
FUNCTIONS: 'DISPLAY:' | 'SCAN:' ;

// TODO: Not final and not implemented
SCAN: 'SCAN:';
scanFunction: SCAN IDENTIFIER (',' IDENTIFIER)* ;

// find a way for the newline when displaying

expression
    : constant                          #constantExpression
    | IDENTIFIER                        #identifierExpression
    | functionCall                      #functionCallExpression
    | '(' expression ')'                #parenthesizedExpression
    | 'NOT' expression                  #notExpression
    | expression multOp expression      #multiplicativeExpression
    | expression addOp expression       #additiveExpression
    | expression compareOp expression   #comparativeExpression
    | expression logicOp expression     #booleanExpression
    | '[' expression ']'                #escapeCodeExpression //add comment to be included
    ; 

// add unary operator
multOp: '*' | '/' | '%' ;
addOp: '+' | '-' | '&' ;
compareOp: '==' | '<>' | '>' | '<' | '>=' | '<='  ;
logicOp: LOGICAL_OPERATOR ;

LOGICAL_OPERATOR: 'AND' | 'OR' | 'NOT' ;

// GOODS
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]* ;
COMMENT: '#' ~[\r\n]* -> skip ;
unknown: SEMI_COLON | BLANK_LINE ;
SEMI_COLON: ';' ;
BLANK_LINE: [ \t]* [\r]? [\n] ; 
WS: [ \t\r]+ -> skip ;