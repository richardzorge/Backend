using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace Console.Commands
{
    public class Interpreter
    {
        private static char[] trimChars=new char[] { '\''};
        private static NLog.Logger Logger
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }
        public delegate void TVoid_Void();
        public delegate void TVoid_String(string UserName);
        private static Dictionary<string, TVoid_Void> RaiseDelegates = new Dictionary<string, TVoid_Void>()
        {
            { "add", Interpreter.RaiseAddUser}
            ,{ "setpassword", Interpreter.RaiseSetUserPassword}
            ,{ "testpassword", Interpreter.RaiseTestUserPassword}
            ,{ "enable", Interpreter.RaiseEnableUser}
            ,{ "disable", Interpreter.RaiseDisableUser}
            ,{ "delete", Interpreter.RaiseDeleteUser}
            ,{ "del", Interpreter.RaiseDeleteUser}
            ,{ "list", Interpreter.RaiseUserList}
            ,{ "exit", Interpreter.RaiseExit}
            ,{ "quit", Interpreter.RaiseExit}
            ,{ "close", Interpreter.RaiseExit}
            ,{ "help", Interpreter.RaiseHelp}
        };

        private static string UserName=string.Empty;

        #region //Add user
        private static TVoid_String _OnAddUser;
        public static event TVoid_String OnAddUser
        {
            add
            {
                _OnAddUser += value;
            }
            remove
            {
                _OnAddUser -= value;
            }
        }

        public static void RaiseAddUser()
        {
            Logger.Trace("RaiseAddUser IN");
            if (_OnAddUser == null)
                return;
            try
            {
                RaiseCommandSyntaxError("ADD");
                Logger.Debug("Adding user {0}", UserName);
                _OnAddUser(UserName.Trim(trimChars));
            }
            catch(Exception ex)
            {
                Logger.Error(ex, "Add user error");
            }
            finally
            {
                Logger.Trace("RaiseAddUser OUT");
            }
        }
        #endregion

        #region //Set user password
        private static TVoid_String _OnSetUserPassword;
        public static event TVoid_String OnSetUserPassword
        {
            add
            {
                _OnSetUserPassword += value;
            }
            remove
            {
                _OnSetUserPassword -= value;
            }
        }

        public static void RaiseSetUserPassword()
        {
            Logger.Trace(string.Format("RaiseSetUserPassword IN"));
            if (_OnSetUserPassword == null)
            {
                
                return;
            }
            try
            {
                RaiseCommandSyntaxError("SETPASSWORD");
                Logger.Debug("Set password for user {0}", UserName);
                _OnSetUserPassword(Interpreter.UserName);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Set user password error");
            }
            finally
            {
                Logger.Trace(string.Format("RaiseSetUserPassword OUT"));
            }
        }
        #endregion

        #region //Test user password
        private static TVoid_String _OnTestUserPassword;
        public static event TVoid_String OnTestUserPassword
        {
            add
            {
                _OnTestUserPassword += value;
            }
            remove
            {
                _OnTestUserPassword -= value;
            }
        }

        public static void RaiseTestUserPassword()
        {
            Logger.Trace("RaiseTestUserPassword IN");
            if (_OnTestUserPassword == null)
                return;
            try
            {
                RaiseCommandSyntaxError("TESTPASSWORD");
                Logger.Debug("Test password for user: {0}", UserName);
                _OnTestUserPassword(UserName.Trim(trimChars));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Test user password error");
            }
            finally
            {
                Logger.Trace("RaiseTestUserPassword OUT");
            }
        }
        #endregion

        #region //Enable user
        private static TVoid_String _OnEnableUser;
        public static event TVoid_String OnEnableUser
        {
            add
            {
                _OnEnableUser += value;
            }
            remove
            {
                _OnEnableUser -= value;
            }
        }

        public static void RaiseEnableUser()
        {
            Logger.Trace("RaiseEnableUser IN");
            if (_OnEnableUser == null)
                return;
            try
            {
                RaiseCommandSyntaxError("ENABLE");
                Logger.Debug("Enable user {0}", UserName);
                _OnEnableUser(UserName.Trim(trimChars));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Enable user error");
            }
            finally
            {
                Logger.Trace("RaiseEnableUser OUT");
            }
        }
        #endregion

        #region //Disable user
        private static TVoid_String _OnDisableUser;
        public static event TVoid_String OnDisableUser
        {
            add
            {
                _OnDisableUser += value;
            }
            remove
            {
                _OnDisableUser -= value;
            }
        }

        public static void RaiseDisableUser()
        {
            Logger.Trace("RaiseDisableUser IN");
            if (_OnDisableUser == null)
                return;
            try
            {
                RaiseCommandSyntaxError("Disable");
                Logger.Debug("Disable user {0}", UserName);
                _OnDisableUser(UserName.Trim(trimChars));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Disable user error");
            }
            finally
            {
                Logger.Trace("RaiseDisableUser OUT");
            }
        }
        #endregion

        #region //Delete user
        private static TVoid_String _OnDeleteUser;
        public static event TVoid_String OnDeleteUser
        {
            add
            {
                _OnDeleteUser += value;
            }
            remove
            {
                _OnDeleteUser -= value;
            }
        }

        public static void RaiseDeleteUser()
        {
            Logger.Trace("RaiseDeleteUser IN");
            if (_OnDeleteUser == null)
                return;
            try
            {
                RaiseCommandSyntaxError("DEL");
                Logger.Debug("Delete user {0}", UserName);
                _OnDeleteUser(UserName.Trim(trimChars));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Delete user error");
            }
            finally
            {
                Logger.Trace("RaiseDeleteUser OUT");
            }
        }
        #endregion

        #region //User list
        private static TVoid_Void _OnUserList;
        public static event TVoid_Void OnUserList
        {
            add
            {
                _OnUserList += value;
            }
            remove
            {
                _OnUserList -= value;
            }
        }

        public static void RaiseUserList()
        {

            Logger.Trace("RaiseUserList IN");
            if (_OnUserList == null)
                return;
            try
            {
                _OnUserList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "User list error");
            }
            finally
            {
                Logger.Trace("RaiseUserList OUT");
            }
        }
        #endregion
        #region //exit
        private static TVoid_Void _OnExit;
        public static event TVoid_Void OnExit
        {
            add
            {
                _OnExit += value;
            }
            remove
            {
                _OnExit -= value;
            }
        }

        public static void RaiseExit()
        {
            Logger.Trace("RaiseExit IN");
            if (_OnExit == null)
                return;
            try
            {
                _OnExit();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Exit error");
            }
            finally
            {
                Logger.Trace("RaiseExit OUT");
            }
        }
        #endregion

        #region //Unknow command
        private static TVoid_String _OnUnknowCommand;
        public static event TVoid_String OnUnknowCommand
        {
            add
            {
                _OnUnknowCommand += value;
            }
            remove
            {
                _OnUnknowCommand -= value;
            }
        }

        public static void RaiseUnknowCommand()
        {

            Logger.Trace("RaiseUnknowCommand IN");
            if (_OnUnknowCommand == null)
                return;
            try
            {
                _OnUnknowCommand(UserName.Trim(trimChars));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Unknow command error");
            }
            finally
            {
                Logger.Trace("RaiseUnknowCommand OUT");
            }
        }
        #endregion

        #region //Command syntax error
        private static TVoid_String _OnCommandSyntaxError;
        public static event TVoid_String OnCommandSyntaxError
        {
            add
            {
                _OnCommandSyntaxError += value;
            }
            remove
            {
                _OnCommandSyntaxError -= value;
            }
        }

        public static void RaiseCommandSyntaxError(string Command)
        {
            if (_OnCommandSyntaxError == null || string.IsNullOrEmpty(UserName))
            {
                Logger.Debug("Empty user name");
                return;
            }
            Logger.Trace("RaiseCommandSyntaxError IN");
            try
            {
                _OnCommandSyntaxError(Command);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Syntax error");
            }
            finally
            {
                Logger.Trace("RaiseCommandSyntaxError OUT");
            }
        }
        #endregion

        #region //Help command
        private static TVoid_Void _OnHelp;
        public static event TVoid_Void OnHelp
        {
            add
            {
                _OnHelp += value;
            }
            remove
            {
                _OnHelp -= value;
            }
        }

        public static void RaiseHelp()
        {

            Logger.Trace("RaiseHelp IN");
            if (_OnHelp == null)
                return;
            try
            {
                _OnHelp();
            }
            catch (Exception ex)
            {
                Logger.Error(ex,"Help command error");
            }
            finally
            {
                Logger.Trace("RaiseHelp OUT");
            }
        }
        #endregion


        public static void ProcessLine(string Line)
        {
            Antlr4.Runtime.AntlrInputStream stream = new AntlrInputStream(Line);
            commandsLexer lexer = new commandsLexer(stream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            commandsParser parser = new commandsParser(tokens);
            commandsParser.ComandContext context = parser.comand();
            
            if(context==null)
            {
                Logger.Error("Command processing error");
                return;
            }
            ITerminalNode command = context.CMD();
            if(command==null)
            {
                Logger.Error("Command processing error");
                return;
            }
            string cmd = command.GetText();
            if(string.IsNullOrEmpty(cmd))
            {
                Logger.Error("Command processing error. Empty command");
                return;
            }

            if(!RaiseDelegates.ContainsKey(cmd.ToLower()))
            {
                Logger.Debug("Unknow command: {0}", cmd);
                return;
            }

            ITerminalNode userName = context.USER_NAME();
            if (userName==null)
            {
                Logger.Debug("UserName: NULL");
            }
            else
            {
                UserName = userName.GetText().ToLower().Trim(trimChars);
                Logger.Debug("UserName: {0}", UserName);
            }

            RaiseDelegates[cmd.ToLower()]();
            UserName = string.Empty;
        }
    }
}
