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

            //For processing exit scenarios
            MegasploitFramework msf = null;
            bool successfulTelnet = false;
            bool megasploitExitCondition = false;

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
                {
                    Console.Write("Are you sure you wish to quit the application? (y/n) ");
                    string response = Console.ReadLine();

                    if (response == "y" || response == "Y")
                    {
                        Console.WriteLine("Quitting Application...");
                        System.Threading.Thread.Sleep(2000);
                        Environment.Exit(0);
                    }
                    else if (response == "n" || response == "N")
                        Console.WriteLine();
                    else
                        Console.WriteLine("Didn't know it was that hard to type the letter Y or N but fine, be that way...\n");
                }

                //Restarts the simulation
                else if(input == "restart")
                {
                    var restart = new ReplaySimulation();
                    restart.RestartSimulation();
                }

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
                    catch (Exception)
                    {
                        invalidInput = true;
                    }

                    //If the input is invalid, print to screen
                    if (invalidInput)
                        Console.WriteLine("That is not a valid command.\n");

                    //Prints the Telnet Help Menu
                    else if (lastArguement == "-h")
                    {
                        var telnet = new Telnet();
                        telnet.Help();
                    }

                    //If the first character of the last arguement is a number, execute
                    else if (Char.IsDigit(firstCharOfLastArguement))
                    {
                        Target telnetTarget = app.FindTarget(lastArguement);
                        var telnet = new Telnet(telnetTarget);

                        telnet.Connect();

                        successfulTelnet = telnet.target.remotelyConnected;
                    }

                    //Else the command is invalid
                    else
                        Console.WriteLine("That is not a valid command.\n");
                }

                //Ping Commands
                else if (input.StartsWith("ping"))
                {
                    bool invalidInput = false; //Checks for invalid input in the string

                    //Splits the arguements passed to the Ping module
                    char firstCharOfLastArguement = ' ';
                    string[] pingSplit = input.Split(splitDelimiters);
                    string lastArguement = pingSplit[pingSplit.Length - 1];

                    //Tries to get the first character of the last arguement
                    try
                    {
                        firstCharOfLastArguement = lastArguement[0];
                    }
                    catch (Exception)
                    {
                        invalidInput = true;
                    }

                    //If invalidInput is true, print error message
                    if (invalidInput)
                        Console.WriteLine("That is not a valid command.\n");

                    //If the last arguement is "-h", print the help menu for the Ping module
                    else if (lastArguement == "-h")
                    {
                        var ping = new Ping();
                        ping.Help();
                    }

                    //If the first character of the last arguement is a number then parse for the Scan
                    else if (Char.IsDigit(firstCharOfLastArguement))
                    {
                        Target pingTarget = app.FindTarget(lastArguement);
                        var ping = new Ping(pingTarget);

                        ping.Scan();
                    }

                    //Else, the command is invalid             
                    else
                        Console.WriteLine("That is not a valid command.\n");
                }

                //Portscan Commands
                else if (input.StartsWith("portscan"))
                {
                    bool invalidInput = false; //Checks for invalid input

                    //Splits the arguments passedto the Portscan module
                    char firstCharacterOfLastArgument = ' ';
                    string[] portscanSplit = input.Split(splitDelimiters);
                    string lastArguement = portscanSplit[portscanSplit.Length - 1];

                    //Tries to get the first character of the last arguement
                    try
                    {
                        firstCharacterOfLastArgument = lastArguement[0];
                    }
                    catch (Exception)
                    {
                        //if no valid last arguement (i.e. a space), set invalidInput
                        invalidInput = true;
                    }

                    //If invalidInput is true, print error to screen
                    if (invalidInput)
                        Console.WriteLine("That is not a valid command.\n");

                    //If the last argument is -h, print the help menu for the Portscan module
                    else if (lastArguement == "-h")
                    {
                        var ps = new Portscan();
                        ps.Help();
                    }

                    //If the first character of the last argument is a number, then execute
                    else if (Char.IsDigit(firstCharacterOfLastArgument))
                    {
                        Target psTarget = app.FindTarget(lastArguement);
                        var ps = new Portscan(psTarget);

                        string arguments = input.Substring(portscanSplit[0].Length + 1);
                        ps.Scan(arguments);
                    }

                    //Else, the command is invalid
                    else
                        Console.WriteLine("That is not a valid command.\n");
                }

                //ExploitDB Commands
                else if (input.StartsWith("edb"))
                {
                    //Splits the arguements
                    string[] edbSplit = input.Split(splitDelimiters);
                    string lastArguement = edbSplit[edbSplit.Length - 1];

                    //Copy of ExploitDB
                    var db = app.exploitDatabase;

                    //Prints the help menu for the ExploitDB module
                    if (lastArguement == "-h")
                    {
                        db.Help();
                    }

                    else if (edbSplit[1] == "service")
                    {
                        try
                        {
                            string serviceName = edbSplit[2];
                            db.SearchService(serviceName);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("That is not a valid command.\n");
                        }
                    }

                    else if (edbSplit[1] == "exploit")
                    {
                        try
                        {
                            string exploitName = edbSplit[2];
                            db.SearchExploit(exploitName);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("That is not a valid command.\n");
                        }
                    }
                    //Else, the command is invalid
                    else
                        Console.WriteLine("That is not a valid command.\n");
                }

                //Launches the Megasploit Framework
                else if (input == "msf start")
                {
                    msf = new MegasploitFramework(app.targets, app.exploitDatabase);
                    msf.Terminal();

                    megasploitExitCondition = msf.exitCondition;
                }

                //Else, command is invalid
                else
                {
                    Console.WriteLine("That is not a valid command.\n");
                }


                /*Process Exit scenario if condition met*/
                
                //If the megasploit framework condition is met, display exit scenario
                if(megasploitExitCondition)
                {
                    ParseExploitForExitScenario(msf);
                    break;
                }
                //Process telnet connection exit scenario if condition is met
                else if(successfulTelnet)
                {
                    var sqlInjectionExit = new ExitScenarios();
                    sqlInjectionExit.SQLInjectionScenario();
                    break;
                }                
            }
            //Ask the user is they wish to replay the simulation with new targets/exploits
            var rerunSim = new ReplaySimulation();
            rerunSim.RerunSimulation();
        }

        //Parses the exploit from the Megasploit framework and displays the appropriate scenario
        private void ParseExploitForExitScenario(MegasploitFramework msf)
        {
            MSExploit exploit = msf.exModule.createdExploit;                       

            var exitScenario = new ExitScenarios(exploit);
            exitScenario.DisplayScenario();
        }

        //Help menu for app
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
            string megaUsage1 = "Megasploit Framework: msf start\n";
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
