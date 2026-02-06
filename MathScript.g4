grammar MathScript;

prog : stat* EOF ;
stat
	: expr ';'
	| scope
	| if_stat
	| while_stat
	| for_stat
	| func_def
	| KEYWORD_RET expr? ';'
	| KEYWORD_BREAK ';'
	| KEYWORD_CONTINUE ';'
	| switch_stat
	| KEYWORD_IMPORT ID ';'
	| class_def
	;
expr
	: type? ID '=' expr # assign
	| or_expr	        # logic
	;
or_expr   : and_expr ('or' and_expr)* ;
and_expr  : comp_expr ('and' comp_expr)* ;
comp_expr : addition (('<=' | '<' | '>=' | '>' | '!=' | '==' | KEYWORD_IN) addition)* ;
addition  : term (('+' | '-') term)* ;
term      : factor (('*' | '/' | '%') factor)* ;
factor    : atom ('^' factor)? | ('+' | '-' | '!') factor ;
atom
	: '(' expr ')'				       # priority_expr
	| '||' expr '||'			       # card
	| '|' expr '|'  			       # abs
	| (ID | PRIMITIVE_TYPE)			   # id
	| literal					       # lit
	| atom '_' '(' expr ')'            # indexing
	| atom '.' (ID | literal)          # attributing
	| atom '(' (expr (',' expr)*)? ')' # func_call
	;
literal
	: NUMBER
	| STRING
	| BOOLEAN
	| NULL
	| sequence | set | vector
	| lambda
	;
type         : (PRIMITIVE_TYPE | ID) ('<' (type | literal | ID) (',' (type | literal | ID))* '>')? ;
scope        : '{' stat* '}' ;
if_stat      : KEYWORD_IF '(' expr ')' scope (KEYWORD_ELSE KEYWORD_IF '(' expr ')' scope)* (KEYWORD_ELSE scope)? ;
while_stat   : KEYWORD_WHILE '(' expr ')' scope ;
for_stat     : KEYWORD_FOR '(' expr? ';' expr? ';' expr? ')' scope ;
switch_stat  : KEYWORD_SWITCH '(' expr ')' '{' case_stat* default_stat? '}' ;
case_stat    : KEYWORD_CASE expr scope ;
default_stat : KEYWORD_DEFAULT scope ;
func_def     : KEYWORD_FUNC type ID '(' type ID ('=' atom)? (',' type ID ('=' atom)?)* ')' scope ;
class_def    : KEYWORD_CLASS ID ('(' type ID (',' type ID)* ')')? (PRIMITIVE_TYPE | ID)? scope ;

sequence : '(' (expr ',' (expr (',' expr)*)?)? ')' ;
set      : '{' ((expr ',' (expr (',' expr)*)?)? | set_builder) '}' ;
vector   : '[' (expr (',' expr)*)? ']' ;

set_builder : expr '|' expr ;

lambda : '(' 'lambda' type '(' type ID ('=' atom)? (',' type ID ('=' atom)?)* ')' scope ')' ;

fragment NUMBER_BASE
	: [0-9]+ ('.' [0-9]+)?
	| '0x' [0-9a-fA-F]+ ('.' [0-9a-fA-F]+)?
	| '0b' [01]+ ('.' [01]+)?
	| '0o' [0-7]+ ('.' [0-7]+)?
	;

fragment BASE_ESC
	: [\\abefnrtv]
	| 'x' [0-9a-fA-F] [0-9a-fA-F]
	| 'u' [0-9a-fA-F] [0-9a-fA-F] [0-9a-fA-F] [0-9a-fA-F]
	  ([0-9a-fA-F] [0-9a-fA-F] [0-9a-fA-F] [0-9a-fA-F])?
	;
fragment STRING_BODY_SG : ('\\' (BASE_ESC | '\'') | ~([\\\r\n] | '\''))* ;
fragment STRING_BODY_DB : ('\\' (BASE_ESC | '"' ) | ~([\\\r\n] | '"' ))* ;
fragment STRING_BODY_BT : ('\\' (BASE_ESC | '`' ) | ~([\\\r\n] | '`' ))* ;

STRING
	: '\'' STRING_BODY_SG '\''
	| '"' STRING_BODY_DB '"'
	| '`' STRING_BODY_BT '`'
	;

NUMBER : NUMBER_BASE (
	'onklm' | 'onjlm' | 'onilm' | 'onlm' | 'onkm' | 'onjm' | 'onim' |
	'onkl'  | 'onjl'  | 'onil'  | 'onm'  | 'onl'  | 'onk'  | 'onj'  |
	'oni'   | 'on'    | 'oklm'  | 'ojlm' | 'oilm' | 'olm'  | 'okm'  |
	'ojm'   | 'oim'   | 'okl'   | 'ojl'  | 'oil'  | 'om'   | 'ol'   |
	'ok'    | 'oj'    | 'oi'    | 'o'    | 'nklm' | 'njlm' | 'nilm' |
	'nlm'   | 'nkm'   | 'njm'   | 'nim'  | 'nkl'  | 'njl'  | 'nil'  |
	'nm'    | 'nl'    | 'nk'    | 'nj'   | 'ni'   | 'n'    | 'klm'  |
	'jlm'   | 'ilm'   | 'lm'    | 'km'   | 'jm'   | 'im'   | 'kl'   |
	'jl'    | 'il'    | 'm'     | 'l'    | 'k'    | 'j'    | 'i'
)?;

BOOLEAN          : 'true' | 'false' ;
NULL  		     : 'null' | 'none' | 'undefined' | 'unknown' ;
KEYWORD_IF       : 'if' ;
KEYWORD_ELSE     : 'else' ;
KEYWORD_WHILE    : 'while' ;
KEYWORD_FOR      : 'for' ;
KEYWORD_FUNC     : 'func' ;
KEYWORD_IN       : 'in' ;
KEYWORD_RET      : 'return' ;
KEYWORD_BREAK    : 'break' ;
KEYWORD_CONTINUE : 'continue' ;
KEYWORD_SWITCH	 : 'switch' ;
KEYWORD_CASE	 : 'case' ;
KEYWORD_DEFAULT  : 'default' ;
KEYWORD_IMPORT   : 'import' ;
KEYWORD_CLASS    : 'class' ;
PRIMITIVE_TYPE
	: 'natural'         | 'N'
	| 'relative'		| 'Z' | 'int'
	| 'rational'		| 'Q'
	| 'real'			| 'R' | 'float' | 'number'
	| 'complex'			| 'C'
	| 'quaternion'		| 'H'
	| 'octonion'		| 'O'
	| 'sedenion'		| 'S'
	| 'trigintaduonion' | 'T'
	| 'sequence'
	| 'set'
	| 'vector' | 'matrix'
	| 'string'
	| 'bool'
	| 'null_t'
	| 'any'
	| 'type'
	;
ID      : [A-Za-z_][A-Za-z0-9_]* ;
COMMENT : (('#' | '//') ~[\r\n]* | '/*' .*? '*/') -> skip;
WS      : [\p{White_Space}] -> skip ;