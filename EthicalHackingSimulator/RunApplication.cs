using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    public class RunApplication : ITerminal
    {
        private Application app;

        public RunApplication()
        {
            app = new Application();
        }

        public void StartApp()
        {
            Terminal();
        }

        public void Terminal()
        {
            string tag = "root@HackSim:~# ";
            char[] splitDelimiters = { ' ' };

            while (true)
            {
                Console.Write(tag);
                string input = Console.ReadLine();

                if (input == "help")
                    PrintHelp();

                else if (input == "quit")
                    Environment.Exit(0);

                else
                    Console.WriteLine("That is not valid input");
            }
        }

        public void PrintHelp()
        {
            Console.WriteLine("Printing Help Menu...");
        }
    }
}
