using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    public class MegasploitFramework : ITerminal
    {
        private TargetList tList;
        private ExploitDB exDB;

        public MegasploitFramework(TargetList tl, ExploitDB exDB)
        {
            this.tList = tl;
            this.exDB = exDB;
        }

        //The console that will be used for the framework
        public void Terminal()
        {
            InitText();

            string tag = "msf > ";
            char[] splitDelimiters = { ' ' };

            while(true)
            {
                Console.Write(tag);
                string input = Console.ReadLine();

                //Exits the framework
                if (input == "exit")
                {
                    Console.WriteLine("Quitting Megasploit Framework...\n");
                    System.Threading.Thread.Sleep(1500);
                    break;
                }

                //Prints the Megasploit Framework Help Menu
                else if(input == "help")
                {
                    PrintHelp();
                }

                //Loads up a module
                else if(input.StartsWith("module"))
                {
                    //args[1] will always have to be the module name
                    string[] args = input.Split(splitDelimiters);

                    //Loads the Exploit Module
                    if (args[1] == "exploit")
                    {
                        var exModule = new MSModule_Exploit(exDB, tList);
                        exModule.Terminal();
                    }

                    //Additional modules can be added here in future works
                    /****************************************************/

                    //Else, not a valid module
                    else
                    {
                        Console.WriteLine("That is not a valid module.\n");
                    }
                }

                //Else, not a valid command
                else
                {
                    Console.WriteLine("That is not a valid command.\n");
                }
            }
        }


        //Prints the initiation text
        private void InitText()
        {
            Console.WriteLine("\nLoading Megasploit Framework...\n");
            System.Threading.Thread.Sleep(3000);

            AsciiArt();
        }

        //ASCII art for flavour
        private void AsciiArt()
        {
            string logoPt1 = "\t\t                         __       _       _ _  \n";
            string logoPt2 = "\t\t  /\\/\\   ___  __ _  __ _/ _\\_ __ | | ___ (_) |_ \n";
            string logoPt3 = "\t\t /    \\ / _ \\/ _` |/ _` \\ \\| '_ \\| |/ _ \\| | __|\n";
            string logoPt4 = "\t\t/ /\\/\\ \\  __/ (_| | (_| |\\ \\ |_) | | (_) | | |_ \n";
            string logoPt5 = "\t\t\\/    \\/\\___|\\__, |\\__,_\\__/ .__/|_|\\___/|_|\\__|\n";
            string logoPt6 = "\t\t             |___/         |_|                  \n";

            string logo = logoPt1 + logoPt2 + logoPt3 + logoPt4 + logoPt5 + logoPt6;

            Console.WriteLine(logo);
        }

        //Prints the help menu for the framework
        public void PrintHelp()
        {
            Console.WriteLine("Printing help menu...");
        }
    }
}
