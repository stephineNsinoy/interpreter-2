﻿grammar Code;

program: lineBlock EOF ;

BEGIN_CODE: 'BEGIN CODE' ;
END_CODE: 'END CODE' ;

// GOODS
BEGIN_IF: 'BEGIN IF' ;
END_IF: 'END IF' ;

lineBlock: BEGIN_CODE declaration* line* END_CODE ;

line: statement | ifBlock | whileBlock | elseIfBlock | elseBlock ;

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


ifBlock: 'IF' '('expression')' BEGIN_IF line* END_IF elseIfBlock? elseBlock? ;
elseIfBlock: 'ELSE IF' '('expression')' BEGIN_IF line* END_IF elseIfBlock? elseBlock ;
elseBlock: 'ELSE' BEGIN_IF line* END_IF ;

// GOODS
WHILE: 'WHILE' ;
BEGIN_WHILE: 'BEGIN WHILE' ;
END_WHILE: 'END WHILE' ;
whileBlock: WHILE '(' expression ')' BEGIN_WHILE line* END_WHILE ;

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
    | expression multOp expression      #multiplicativeExpression
    | expression addOp expression       #additiveExpression
    | expression compareOp expression   #comparativeExpression
    | expression logicOp expression     #booleanExpression
    | ESCAPE                            #escapeCodeExpression // still needs to be tested
    | NEXTLINE                          #nextLineExpression
    ;

multOp: '*' | '/' | '%' ;
addOp: '+' | '-' | '&' ;
compareOp: '==' | '<>' | '>' | '<' | '>=' | '<='  ;
logicOp: LOGICAL_OPERATOR ;

LOGICAL_OPERATOR: 'AND' | 'OR' ;

// GOODS
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]* ;
ESCAPE: '[' .*? ']' ;
NEXTLINE: '$' ;
COMMENT: '#' ~[\r\n]* -> skip ;
WS: [ \t\r]+ -> skip ;

// STILL NEEDS TO BE TESTED
unknown: SEMI_COLON | BLANK_LINE ;
SEMI_COLON: ';' ;
BLANK_LINE: [ \t]* [\r]? [\n] ; 