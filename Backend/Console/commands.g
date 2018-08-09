grammar commands;

options
{
	language = CSharp;
}


fragment ID  :	('a'..'z'|'A'..'Z'|'0'..'9'|'_'|'.'|'-')
    ;

WS  :    ( ' '| '\t')+ 
    ;
    
ENDLINE :		   
         ( '\r'? '\n')
    ;

USER_NAME
	:	(('\''  ID* '@'  ID* '\'' ) | ('\''  ID* '\''))
	;

CMD
	:	('ADD'|'add')
		|('setpassword'|'SETPASSWORD')
		|('ENABLE'|'enable')
		|('DISABLE'|'disable')
		|('TESTPASSWORD'|'testpassword')
		|('del'|'DEL')
		|('list'|'LIST')
		|('help'|'HELP')
		|('exit'|'EXIT')
		|('quit'|'QUIT')
		|('close'|'CLOSE')
	;
	
	
comand
	: (CMD WS+ (USER_NAME WS+)? ENDLINE)
	;
