using System;
using Backend.Models;
using Antlr4;
using Antlr4.Runtime;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace Console
{
    class Program
    {
        static  NLog.Logger Logger
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }
        static bool Work = true;
        static void Main(string[] args)
        {
            string h = string.Format("{0}@{1} : ", Environment.UserName, Environment.MachineName);
            if (!Init())
                return;
            Logger.Info("Console started");
            while (Work)
            {
                System.Console.Write(h);
                string inputLine = System.Console.ReadLine()+" \r\n";
                Commands.Interpreter.ProcessLine(inputLine);
            }
            System.Console.WriteLine("Buy. See you later :)");
            Thread.Sleep(3000);
        }

        private static bool Init()
        {
            try
            {
                Settings.Create();
            }
            catch(Exception ex)
            {
                Logger.Error(ex, "Configuration load error");
                return false;
            }

            
            Commands.Interpreter.OnExit += Interpreter_OnExit;
            Commands.Interpreter.OnUnknowCommand += Interpreter_OnUnknowCommand;
            Commands.Interpreter.OnHelp += Interpreter_OnHelp;
            Commands.Interpreter.OnAddUser += Interpreter_OnAddUser;
            Commands.Interpreter.OnUserList += Interpreter_OnUserList;
            Commands.Interpreter.OnSetUserPassword += Interpreter_OnSetUserPassword;
            Commands.Interpreter.OnTestUserPassword += Interpreter_OnTestUserPassword;
            Commands.Interpreter.OnDeleteUser += Interpreter_OnDeleteUser;
            Commands.Interpreter.OnEnableUser += Interpreter_OnEnableUser;
            Commands.Interpreter.OnDisableUser += Interpreter_OnDisableUser;
            return true;
        }

        private static void Interpreter_OnAddUser(string UserName)
        {
            try
            {
                System.Console.WriteLine(string.Format("User login '{0}'", UserName));
                System.Console.WriteLine(string.Format("Enter password for user '{0}':", UserName));
                string password = string.Empty;
                try
                {
                    password = InputPassword();
                }
                catch (Exception ex)
                {
                    Logger.Info(ex, "");
                    return;
                }
                System.Console.WriteLine("Please enter full user name");
                string fullUserName = System.Console.ReadLine();
                System.Console.WriteLine("Please enter comment (if you need)");
                string comment = System.Console.ReadLine();
                Admins.Add(UserName, password, fullUserName, comment);
            }
            catch(Exception ex)
            {
                System.Console.WriteLine($"Add user error: {ex.Message}");
                Logger.Error(ex,"Create user error");
            }
        }
        private static char[] trimChars = new char[] { '\r','\n'};
        private static string InputPassword()
        {
            string result = string.Empty;
            System.ConsoleKeyInfo info;
            do
            {
                int left = System.Console.CursorLeft;
                int top = System.Console.CursorTop;
                info = System.Console.ReadKey();
                if (info.Key == ConsoleKey.Backspace)
                {
                    // Are there any characters to erase?
                    if (result.Length >= 1)
                    {
                        result = result.Substring(0, result.Length - 1);
                        System.Console.SetCursorPosition(left - 1, top);
                        System.Console.Write(" ");
                        System.Console.SetCursorPosition(left-1, top);
                    }
                    continue;
                }
                if (isCharKey(info))
                {
                    result += info.KeyChar.ToString();
                    System.Console.SetCursorPosition(left, top);
                    System.Console.Write("*");
                }
                else
                    System.Console.SetCursorPosition(left, top);

            }
            while (info.Key != System.ConsoleKey.Enter && info.Key != System.ConsoleKey.Escape);
            System.Console.WriteLine("");
            if (info.Key == System.ConsoleKey.Escape)
                throw new Exception("Cancel");
            return result.Trim(trimChars);
        }
        static bool isCharKey(System.ConsoleKeyInfo info)
        {
            if ((info.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt
                || (info.Key == ConsoleKey.Tab)
                || (info.Key == ConsoleKey.Enter)
                || (info.Key == ConsoleKey.Escape)
                || (info.Key == ConsoleKey.EraseEndOfFile)
                || (info.Key == ConsoleKey.CrSel)
                || (info.Key == ConsoleKey.Applications)
                || (info.KeyChar == '\u0000')
                )
                return false;
            return true;
        }

        private static void Interpreter_OnEnableUser(string UserName)
        {
            try
            {
                System.Console.WriteLine(string.Format("User '{0}' will be enabled", UserName));
                Admins.Enable(UserName);
            }
            catch (KeyNotFoundException ex)
            {
                System.Console.WriteLine("  User not found");
            }
            catch(Exception ex)
            {
                System.Console.WriteLine($"Enable user error: {ex}");
                Logger.Error(ex, "Enable user error");
            }
        }

        private static void Interpreter_OnDisableUser(string UserName)
        {
            try
            {
                System.Console.WriteLine(string.Format("User '{0}' will be disabled", UserName));
                Admins.Disable(UserName);
            }
            catch (KeyNotFoundException ex)
            {
                System.Console.WriteLine("  User not found");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Disable user error: {ex}");
                Logger.Error(ex, "Disable user error");
            }
        }

        private static void Interpreter_OnDeleteUser(string UserName)
        {
            try
            {
                System.Console.WriteLine(string.Format("User '{0}' will be deleted", UserName));
                System.Console.WriteLine("You are shure? Y/N");
                ConsoleKeyInfo keyInfo = System.Console.ReadKey();
                do
                {
                    System.Console.WriteLine("Pleas enter 'Y' for 'yes' or 'N' for 'no'");
                    keyInfo = System.Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        Admins.Delete(UserName);
                    }
                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        return;
                    }
                }
                while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N);
            }
            catch (KeyNotFoundException ex)
            {
                System.Console.WriteLine("  User not found");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Delete user error: {ex}");
                Logger.Error(ex, "Delete user error");
            }
        }

        private static void Interpreter_OnHelp()
        {
            System.Console.WriteLine("Ok. I can help you :)");
            System.Console.WriteLine("");
            System.Console.WriteLine("Available commands:");
            System.Console.WriteLine("      ADD '<LOGIN>' - add new user");
            System.Console.WriteLine("      DEL '<LOGIN>' - delete user");
            System.Console.WriteLine("      LIST- show all users");
            System.Console.WriteLine("");
            System.Console.WriteLine("      ENABLE '<LOGIN>' - enable user");
            System.Console.WriteLine("      DISABLE '<LOGIN>' - disable user");
            System.Console.WriteLine("");
            System.Console.WriteLine("      SETPASSWORD '<LOGIN>' - set new user password");
            System.Console.WriteLine("      TESTPASSWORD '<LOGIN>' - test user password");
            System.Console.WriteLine("");
            System.Console.WriteLine("      QUIT or CLOSE or EXIT - close program");
            System.Console.WriteLine("");
        }

        private static void Interpreter_OnUnknowCommand(string UserName)
        {
            System.Console.WriteLine("You enter unknow command. Please enter \'help\' :)");
        }

        private static void Interpreter_OnTestUserPassword(string UserName)
        {
            try
            {
                System.Console.WriteLine(string.Format("Enter password for user {0}:", UserName));
                string password = InputPassword();
                System.Console.WriteLine(string.Format("Result: {0}", Admins.TestPassword(UserName, password)));
            }
            catch(KeyNotFoundException ex)
            {
                System.Console.WriteLine("  User not found");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Test user password error: {ex}");
                Logger.Error(ex, "Test user password error");
            }
        }

        private static void Interpreter_OnSetUserPassword(string UserName)
        {
            try
            {
                Admins Item = Admins.GetUserByLogin(UserName);
                if (Item == null)
                {
                    System.Console.WriteLine(string.Format("User {0} not found", UserName));
                    return;
                }
                System.Console.WriteLine(string.Format("Enter new password for user {0}:", UserName));
                Item.Password = InputPassword();
                Admins.AddOrUpdate(Item);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Set user password error: {ex}");
                Logger.Error(ex, "Set user password error");
            }
        }

        private static void Interpreter_OnUserList()
        {
            try
            {
                System.Console.WriteLine("User list:");
                List<Admins> admins = Admins.All();
                int i = 0;
                int x = 0;
                while (x < admins.Count)
                {
                    for (x = i; x < i + 10 && x < admins.Count; x++)
                    {
                        System.Console.WriteLine("  {0}     {1}", admins[x].Email, admins[x].FullName);
                    }
                    if (x == admins.Count - 1)
                    {
                        System.Console.WriteLine("END");
                        return;
                    }
                    else
                        System.Console.WriteLine("MORE?");
                    ConsoleKeyInfo keyInfo = System.Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                    i = x;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"List user error: {ex}");
                Logger.Error(ex, "List user error");
            }
        }

        private static void Interpreter_OnExit()
        {
            Work = false;
        }
    }
}
