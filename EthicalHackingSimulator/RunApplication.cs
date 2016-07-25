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
            app.PrintIntro();
            string tag = "root@HackSim:~# ";
            char[] splitDelimiters = { ' ' };

            while (true)
            {
                Console.Write(tag);
                string input = Console.ReadLine();

                if (input == "help")
                    PrintHelp();

                else if (input == "targets")
                    app.PrintTargets();

                else if (input == "quit")
                    Environment.Exit(0);

                else if (input == "clear")
                    Console.Clear();

                else
                    Console.WriteLine("That is not a valid command.\n");
            }
        }

        public void PrintHelp()
        {
            //Telnet Info
            string tnUsage = "Telnet: telnet {ip address} [port]\n";
            string tnInfo = "Allows for remote access to the target destination.\n\n";
            string telnet = tnUsage + tnInfo;

            //PortScan Info
            string psUsage = "Portscan: portscan [Option(s)] {target}\n";
            string psInfo = "Used to scan the ports on the target to assess services prone to vulnerabilities.\n\t --For more information, type 'portscan -h'\n\n";
            string portscan = psUsage + psInfo;

            //Ping Info
            string pgUsage = "Ping: ping {target}\n";
            string pgInfo = "Sends a signal to the target to determine if it is alive or not.\n\n";
            string ping = pgUsage + pgInfo;

            //ExploitDB Info
            string edbUsage = "ExploitDB: edb [Option(s)] {service}\n";
            string edbInfo = "Scans the service for available exploits.\n\t --For more information, type 'edb -h'\n\n";
            string exploitDB = edbUsage + edbInfo;

            //Metasploit Info
            string megaUsage1 = "Megasploit: msc start\n";
            string megaUsage2 = "Starts the Megasploit Framework console.\n";
            string megaInfo = "Used to create exploits for specific services to be uploaded to a target.\n";
            string megaInfo2 = "\t --For more information, Launch the framework and type 'help'\n\n";
            string megasploit = megaUsage1 + megaUsage2 + megaInfo + megaInfo2;

            string clear = "To clear the screen, type 'clear'\n\n";
            //Exit Info
            string exit = "To exit the application, type 'quit'\n\n";

            Console.WriteLine();
            Console.WriteLine(telnet + ping + portscan + exploitDB + megasploit + clear + exit);
        }
    }
}
