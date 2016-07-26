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

                //Prints the Simulation Help Menu
                if (input == "help")
                    PrintHelp();

                //Prints the IP addresses of the available targets
                else if (input == "targets")
                    app.PrintTargets();

                //Quits the simulation
                else if (input == "quit")
                    Environment.Exit(0);

                //Clears the console screen
                else if (input == "clear")
                    Console.Clear();

                //Initiates a Telnet connection on the target
                else if (input.StartsWith("telnet"))
                {
                    bool invalidInput = false;
                    char firstCharOfLastArguement = ' ';
                    string[] telnetSplit = input.Split(splitDelimiters);
                    string lastArguement = telnetSplit[telnetSplit.Length - 1];

                    try
                    {
                        firstCharOfLastArguement = lastArguement[0];
                    }
                    catch(Exception)
                    {
                        invalidInput = true;
                    }

                    //If the input is invalid, print to screen
                    if(invalidInput)
                        Console.WriteLine("That is not a valid command.\n");

                    //Prints the Telnet Help Menu
                    else if(lastArguement == "-h")
                    {
                        var telnet = new Telnet();
                        telnet.Help();
                    }

                    //If the first character of the last arguement is a number, execute
                    else if(Char.IsDigit(firstCharOfLastArguement))
                    {
                        Target telnetTarget = app.FindTarget(lastArguement);
                        var telnet = new Telnet(telnetTarget);

                        telnet.Connect();
                    }

                    //Else the command is invalid
                    else
                        Console.WriteLine("That is not a valid command.\n");
                }

                //Ping Commands
                else if(input.StartsWith("ping"))
                {
                    //TODO Implement Ping Scan
                }

                //Portscan Commands
                else if(input.StartsWith("portscan"))
                {
                    //TODO Implement Portscan functionality
                }

                //ExploitDB Commands
                else if(input.StartsWith("edb"))
                {
                    //TODO implement Exploit DB functionality
                }

                //Launches the Megasploit Framework
                else if(input == "mscstart")
                {
                    //TODO Implement Megasploit Framework commands
                }

                //Else, command is invalid
                else
                    Console.WriteLine("That is not a valid command.\n");
            }
        }

        public void PrintHelp()
        {
            //Telnet Info
            string tnUsage = "Telnet: telnet {ip address}\n";
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
