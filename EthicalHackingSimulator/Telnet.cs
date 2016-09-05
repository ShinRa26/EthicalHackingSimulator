using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    /// <summary>
    /// This class simulates Telnet functionality.
    /// The user attempts to "connect" to a target, supplying a username and password.
    /// If successful, the user has logged in.
    /// </summary>
    public class Telnet
    {
        public Target target;
        private TargetDB db;
        private bool invalidTarget;

        //Default Constructor
        public Telnet(){}
        
        //Constructor
        public Telnet(Target t)
        {
            //Attempts to parse the target
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

        //Initiates a connection to the target IP
        public void Connect()
        {
            if(invalidTarget)
                Console.WriteLine("That is not a valid target.\n");

            else
            {
                Console.WriteLine("Attempting Telnet Initialisation...\n");
                System.Threading.Thread.Sleep(2000);

                //Reads the given username
                Console.Write("Username: ");
                string uName = Console.ReadLine();

                //Reads the given password
                Console.Write("Password: ");
                string pWord = ReadPassword();

                //If the target does not have a database, there are no login details for the target - will always fail to connect
                if (db == null)
                {
                    Console.WriteLine("Connecting...\n");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("Invalid username/password. \n");
                }

                /* If the target has a database, compares the given username/password to those in the database.
                 * If the username and password match, login will be successful, else will fail.
                 */
                else
                {
                    Console.WriteLine("Connecting...\n");
                    System.Threading.Thread.Sleep(2000);

                    //Local copy of the database table
                    var table = db.table;

                    //Boolean for finding the target
                    bool found = false;

                    //For each Key/Value pair in the table, compare the username to Key and password to the Value
                    foreach(KeyValuePair<string,string> kvp in table)
                    {
                        //If a match, login successful, set remotelyConnected on target to True
                        if(uName == kvp.Key && pWord == kvp.Value)
                        {
                            Console.WriteLine("Login Successfull!\n");
                            target.remotelyConnected = true;
                            found = true;
                            break;
                        }                        
                    }

                    if(!found)
                        Console.WriteLine("Invalid username/password.\n");
                }              
            }
        }

        //Replaces password with *'s on screen and returns the value of the real password (Gold plating tbh...)
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

        //Help Menu
        public void Help()
        {
            string intro1 = "\nTelnet: Allows for the user to log in remotely to the machine.";

            string usage1 = "\n\nUsage: ";
            string usage2 = "telnet {target ip}\n\n";

            string exInfo1 = "Note:\nThis is not an accurate representation of Telnet, this is just a simulation of a remote connection login via the terminal.\n";
            string exInfo2 = "The real Telnet tool allows for remote access to the machine but through different means (the Telnet protocol) other than a username and password combination.\n";
            string exInfo3 = "For more information on Telnet, please see Miccrosoft's own page: \"https://technet.microsoft.com/en-gb/library/bb491013.aspx\"\n";

            string telnet = intro1 + usage1 + usage2 + exInfo1 + exInfo2 + exInfo3;

            Console.WriteLine(telnet);
        }
    }
}
