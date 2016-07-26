using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    public class Telnet
    {
        private Target target;
        private TargetDB db;
        private bool invalidTarget;

        public Telnet(){}

        public Telnet(Target t)
        {
            try
            {
                this.target = t;
                this.db = target.db;
                invalidTarget = false;
            }
            catch(Exception)
            {
                invalidTarget = true;
            }
        }

        public void Connect()
        {
            if(invalidTarget)
                Console.WriteLine("That is not a valid target.\n");

            else
            {
                Console.WriteLine("Connecting...\n");
                System.Threading.Thread.Sleep(2000);

                //Reads the given username
                Console.Write("Username: ");
                string uName = Console.ReadLine();

                //Reads the given password
                Console.Write("Password: ");
                string pWord = ReadPassword(); 
                
                //If the target does not have a database, there are no login details for the target - will always fail to connect
                if(db == null)
                    Console.WriteLine("Invalid username/password. \n");

                /* If the target has a database, compares the given username/password to those in the database.
                 * If the username and password match, login will be successful, else will fail.
                 */
                else
                {
                    //TODO Create TargetDB and compare values
                }              
            }
        }

        //Replaces password with *'s on screen and returns the value of the real password
        private string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);

            while(info.Key != ConsoleKey.Enter)
            {
                if(info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }

                else if(info.Key == ConsoleKey.Backspace)
                {
                    if(!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);

                        int cursorPos = Console.CursorLeft;
                        Console.SetCursorPosition(cursorPos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(cursorPos - 1, Console.CursorTop);
                    }
                }

                info = Console.ReadKey(true);
            }
            Console.WriteLine();
            return password;
        }


        public void Help()
        {
            Console.WriteLine("Printing Help!");
        }
    }
}
