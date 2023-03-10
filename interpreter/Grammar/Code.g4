grammar Code;

program: COMMENT* BEGIN_CODE line* END_CODE COMMENT* EOF ;

BEGIN_CODE: 'BEGIN' 'CODE' ;
END_CODE: 'END' 'CODE' ;

line: declaration | statement | ifBlock | whileBlock | COMMENT* ;
block: line* ;

statement: (assignment | functionCall) ';' ;

declaration: dataType IDENTIFIER (',' IDENTIFIER)* ('=' expression)? ;
assignment: IDENTIFIER '=' expression ;

// GOODS
dataType: INT | FLOAT | STRING | BOOL | CHAR ;

INT: [0-9]+ ;
FLOAT: [0-9]+ '.' [0-9]+ ;
STRING: ('"' ~'"'* '"') | ('\'' ~'\''* '\'') ;
BOOL: '\"TRUE\"' | '\"FALSE\"' ;
CHAR: '\'' ~[\r\n\'] '\'' ; 

// GOODS
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]* ;
COMMENT: '#' ~[\r\n]* -> skip ;
BLANK_LINE: [ \t]* [\r]? [\n] ; 
WS: [ \t\r]+ -> skip ;

// GOODS
BEGIN_IF: 'BEGIN' 'IF' ;
END_IF: 'END' 'IF' ;
ifBlock: 'IF' '('expression')' BEGIN_IF block END_IF elseIfBlock? ;
elseIfBlock: 'ELSE' (BEGIN_IF block END_IF) | ifBlock ;

// GOODS
WHILE: 'WHILE' ;
BEGIN_WHILE: 'BEGIN' 'WHILE' ;
END_WHILE: 'END' 'WHILE' ;
whileBlock: WHILE '(' expression ')' BEGIN_WHILE block END_WHILE ;

// for DISPLAY: and SCAN:
functionCall: IDENTIFIER ':' (expression (',' expression)*)? ;

// Not used
SCAN: 'SCAN:';
scanFunction: SCAN IDENTIFIER (',' IDENTIFIER)* ;

// find a way for the newline when displaying

expression
    : dataType                          #dataTypeExpression
    | IDENTIFIER                        #identifierExpression
    | functionCall                      #functionCallExpression
    | '(' expression ')'                #parenthesizedExpression
    | 'NOT' expression                  #notExpression
    | expression multOp expression      #multiplicativeExpression
    | expression addOp expression       #additiveExpression
    | expression compareOp expression   #comparisonExpression
    | expression logicOp expression     #booleanExpression
    | parenOpen expression parenClose   #escapeCodeExpression //add comment to be included
    ; 

// add unary operator
multOp: '*' | '/' | '%' ;
addOp: '+' | '-' | '&' ;
compareOp: '==' | '<>' | '>' | '<' | '>=' | '<='  ;
logicOp: LOGICAL_OPERATOR ;
parenOpen: '[' ;
parenClose: ']' ;

LOGICAL_OPERATOR: 'AND' | 'OR' | 'NOT' ;
