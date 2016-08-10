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
        public MSModule_Exploit exModule { get; private set; }
        public bool exitCondition { get; private set; }

        public MegasploitFramework(TargetList tl, ExploitDB exDB)
        {
            this.tList = tl;
            this.exDB = exDB;
            exModule = null;
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
                bool exModuleExitCondition = false;

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

                //Clears the console screen
                else if(input == "clear")
                {
                    Console.Clear();
                }

                //Loads up a module
                else if(input.StartsWith("module"))
                {
                    //args[1] will always have to be the module name
                    string[] args = input.Split(splitDelimiters);

                    //Loads the Exploit Module
                    if (args[1] == "exploit")
                    {
                        exModule = new MSModule_Exploit(exDB, tList);
                        exModule.Terminal();
                        exModuleExitCondition = exModule.exitCondition;
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

                //Checks the exit condition of the exploit module
                if(exModuleExitCondition)
                {
                    exitCondition = true;
                    break;
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
            Console.WriteLine("\t\t\tType help for more information!\n");
        }

        //Prints the help menu for the framework
        public void PrintHelp()
        {
            //Title
            string title = "Megasploit Framework Help Menu:\n\n";

            //Module Activation
            string mod1 = "Module launch: module {module name}\n";
            string mod2 = "Launches a module in Megasploit Framework.\n\n";
            string mod3 = "Module Names:\n";
            string mod4 = "exploit -- Exploit module used in the creation and deployment of exploits.\n\n";

            string module = mod1 + mod2 + mod3 + mod4;

            //Exit the framework
            string exit = "To exit the framework and go back to the main console, type 'exit'\n\n";

            //Clears the console
            string clear = "To clear the console, type 'clear'\n\n";

            //Displays the help menu
            string help = title + module + exit + clear;
            Console.WriteLine();
            Console.WriteLine(help);
        }
    }
}
