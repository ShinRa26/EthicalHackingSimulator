using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    public class Portscan
    {
        private Target target;
        private Dictionary<int, string> services;
        private Dictionary<int, string> portStatus;

        //Determines of the target is valid or not
        private bool invalidTarget;

        //Default Constructor
        public Portscan() {}

        //Constructor for scanning the target.
        public Portscan(Target t)
        {
            try
            {
                this.target = t;
                this.services = target.portsAndServices;
                this.portStatus = target.portStatus;

                //If a valid target is passed, initialised to false
                invalidTarget = false;
            }
            catch(Exception)
            {
                //If an invalid target is passed, initialised to true
                invalidTarget = true;
            }
        }
        
        //Scans the target for it's services and ports
        public void Scan(string args)
        {
            //If the target is invalid (i.e. not a valid IP address) then prints error to the console
            if(invalidTarget)
                Console.WriteLine("That is not a valid target.\n");

            //Else, if the target is alive(online) AND valid, the scan will commence
            else if(target.isAlive)
            {
                //Booleans to set f the arguements are present
                bool individualOutput = false;
                bool fragment = false;
                bool verbose = false;

                //Initialised port number to 0 if the -p arguement is passed
                int portNumber = 0;

                //Splits the arguements
                char[] splitDelimiter = { ' ' };
                string[] argSplit = args.Split(splitDelimiter);

                Console.WriteLine("\nInitialising Portscan on {0}...\n", target.ipAddress);
                System.Threading.Thread.Sleep(3000);

                //Loops through the arguements and sets their boolean equivalents if present
                for(int i = 0; i < argSplit.Length; i++)
                {
                    //For verbose output
                    if (argSplit[i] == "-v")
                        verbose = true;

                    //For fragmetation of packets
                    else if (argSplit[i] == "-f")
                        fragment = true;

                    //For individual ports
                    else if (argSplit[i] == "-p")
                    {
                        individualOutput = true;

                        //Parses the port number after the -p arguement
                        try
                        {
                            portNumber = Int32.Parse(argSplit[i + 1]);
                        }
                        catch (Exception)
                        {                           
                            break;
                        }
                    }

                    //Ignores the ip address at the end of the arguements
                    else if (argSplit[i] == argSplit[argSplit.Length - 1])
                        continue;
                }

                /* This section deals with comparing the values of the booleans previously set
                */

                //If all three arguements are true then execute all three methods
                if (verbose == true && fragment == true && individualOutput == true)
                {
                    //Individual port scan performs its own verbose output so VerboseFull() omitted
                    //If port number is greater than 0, perform scan
                    if (portNumber > 0)
                    {
                        Fragment();
                        IndividualPort(portNumber);
                    }
                    //Else, the port number is invalid
                    else
                        Console.WriteLine("That is not a valid port number.\n");
                }

                //If verbose and individualOutput are true, then execute verbose and Individual Port scan
                else if (verbose == true && individualOutput == true)
                {
                    //IndividualPort performs verbose output on its own, so VerboseFull() omitted

                    //If the port number is greater than 0, then execute
                    if (portNumber > 0)
                        IndividualPort(portNumber);

                    //Else, the port number is invalid
                    else
                        Console.WriteLine("That is not a valid port number.\n");
                }

                //If verbose and fragment are tur, then execute VerboseFull() & Fragment(0 methods and print the output
                else if (verbose == true && fragment == true)
                {
                    VerboseFull();
                    Fragment();
                    Output();
                }

                //If fragment and individualOutput are true, then execute Fragment() & IndividualPort() methods
                else if (fragment == true && individualOutput == true)
                {
                    //If the port number is greater than 0, then execute
                    if (portNumber > 0)
                    {
                        Fragment();
                        IndividualPort(portNumber);
                    }

                    //Else, the port number is invalid
                    else
                        Console.WriteLine("That is not a valid port number.\n");
                }

                //If only verbose is true, then execute VerboseFull() and print out the Output()
                else if (verbose == true)
                {
                    VerboseFull();
                    Output();
                }

                //If only fragment is true, then execute Fragment() and Output()
                else if (fragment == true)
                {
                    Fragment();
                    Output();
                }

                //If only individualOutput is true, then execute IndividualPort() (VerboseFull() & Output() omitted)
                else if (individualOutput == true)
                {
                    //if the port number is greater than 0, then execute
                    if (portNumber > 0)
                        IndividualPort(portNumber);

                    //Else, the port number is invalid
                    else
                        Console.WriteLine("That is not a valid port number.\n");
                }

                //If no arguements are passed, then just executes Output()
                else
                    Output();
            }

            //Else, the target is not alive
            else
            {
                Console.WriteLine("\nInitiating Portscan on {0}...\n", target.ipAddress);
                System.Threading.Thread.Sleep(3000);

                Console.WriteLine("No response from target.\n");
            }
        } 
        
        //Prints out the Portscan results
        private void Output()
        {
            Console.WriteLine("\nPort \t\tService \t\tStatus");

            for(int i = 0; i < portStatus.Count; i++)
            {
                //If both dictionaries contain i as a key, then process the output
                if (services.ContainsKey(i) && portStatus.ContainsKey(i))
                {
                    //Does not display closed ports
                    if (portStatus[i] != "Closed")
                    {
                        //Fixes string formatting issues...
                        if (i == 22 || i == 23 || i == 25 || i == 80)
                            Console.WriteLine("{0}/tcp \t\t{1} \t\t\t{2}\n", i, services[i], portStatus[i]);
                        else if (i >= 100)
                            Console.WriteLine("{0}/tcp \t{1} \t\t{2}\n", i, services[i], portStatus[i]);
                        else
                            Console.WriteLine("{0}/tcp \t\t{1} \t\t{2}\n", i, services[i], portStatus[i]);
                    }
                }

                //If neither dictionary contains the key, skip to the next iteration
                else
                    continue;
            }

            //By default, any ports not listed in the Services dictionary are said to be closed
            Console.WriteLine("\nAll other ports are closed.\n");
        }
        
        //More detailed output
        public void VerboseFull()
        {
            //Prints out the verbose scan text
            TextForVerbose();

            //Loops through the ports
            for(int i = 0; i < portStatus.Count; i++)
            {
                //If the services and the portStatus dictionaries contain i as a key, execute
                if(services.ContainsKey(i) && portStatus.ContainsKey(i))
                {
                    //If the port status is open, say the port has been discovered
                    if(portStatus[i] == "Open")
                    {
                        Console.WriteLine("Discovered open port {0}/tcp on {1}", i, target.ipAddress);
                        System.Threading.Thread.Sleep(500);
                    }
                }
            }
        }   
        
        //Fragments the "packets" to discern if Filtered ports are Open/Closed
        private void Fragment()
        {
            Random r = new Random();

            Console.WriteLine("Fragmenting packets...");
            System.Threading.Thread.Sleep(2000);

            //Switches "Filtered" ports to "Open" or "closed" depending on the chance (50%)
            for(int i = 0; i < portStatus.Count; i++)
            {
                if(portStatus[i] == "Filtered")
                {
                    int chance = r.Next(0, 2);

                    if (chance == 0)
                        portStatus[i] = "Open";
                    else
                        portStatus[i] = "Closed";
                }
            }            
        }  
        
        //gets the status and service of the given port
        private void IndividualPort(int p)
        {
            TextForVerbose();

            Console.WriteLine("Scanning port {0}...", p);
            System.Threading.Thread.Sleep(1000);

            if(!services.ContainsKey(p))
            {
                Console.WriteLine("\nPort \t\tService \t\tStatus");
                Console.WriteLine("{0} \t\t{1} \t\t{2}\n\n", p, "No Service", portStatus[p]);
            }

            else
            {
                if(portStatus[p] != "Closed")
                {
                    Console.WriteLine("\nPort \t\tService \t\tStatus");

                    //To fix string formatting issues...
                    if(p == 22 || p == 23 || p == 25 || p == 80)
                        Console.WriteLine("{0}/tcp \t\t{1} \t\t\t{2}\n", p, services[p], portStatus[p]);
                    else if(p >= 100)
                        Console.WriteLine("{0}/tcp \t{1} \t\t{2}\n", p, services[p], portStatus[p]);
                    else
                        Console.WriteLine("{0}/tcp \t\t{1} \t\t{2}\n", p, services[p], portStatus[p]);
                }

                //If the port is closed, display message
                else
                    Console.WriteLine("Port is closed. \n");
            }
        }  

        //Method to contain the Verbose text (Used for VerboseFull() & IndividualPort() methods)
        private void TextForVerbose()
        {
            Console.WriteLine("Initiating ARP Ping Scan on {0}", target.ipAddress);
            System.Threading.Thread.Sleep(50);
            Console.WriteLine("Completed ARP Ping Scan on {0}\n", target.ipAddress);
            System.Threading.Thread.Sleep(100);

            Console.WriteLine("Initiating Parallel DNS resolution of host: {0}", target.ipAddress);
            System.Threading.Thread.Sleep(250);
            Console.WriteLine("Completed Parallel DNS resolution of host: {0}", target.ipAddress);
            System.Threading.Thread.Sleep(150);

            Console.WriteLine("Initiating SYN Stealth Scan...");
            System.Threading.Thread.Sleep(150);
        }

        //Prints out the help menu for the Portscan Module
        public void Help()
        {
            string i1 = "\nPortscan: A tool designed to scan the ports of a target to determine what services may be running.";
            string i2 = " Running the scan on an IP Address will show the Port Number, the service running on that port, and the status of the port (Open/Filtered/Closed).\n";
            string intro = i1 + i2;

            string usage = "\nUsage: portscan [Option(s)] {target ip address}\n";

            string ports = "\nPort Status:\n";
            string open = "Open: Port is Open and potentially vulnerable.\n";
            string closed = "Closed: Port is Closed. Service not running.\n";
            string filtered = "Filtered: Port is being filtered by a firewall. Actual status (Open/Closed) unknown.\n";
            string portStatus = ports + open + closed + filtered;

            string args = "\nOptions:\n";
            string arg1 = "-v: Verbose output. Gives a breakdown of what the scan is actually doing.\n";
            string arg2 = "-f: Fragment Packets: Fragments the packets being sent to the ports. Used to determine a filtered port's status.\n";
            string arg3 = "-p {Port Number}: Performs the portscan on the specific port given and no others.\n";
            string arguements = args + arg1 + arg2 + arg3;

            string exInfo = "\nExtra Information:\n";
            string ex1 = "This Portscan tool is not an actual port scanner, it is only a simulation. It is also VASTLY simplified for educational purposes.";
            string ex2 = " For more information on a real-world Portscanner, see the NMap scanner.\n\n";
            string ex3 = "NMap Website: https://nmap.org\n";
            string extraInfo = exInfo + ex1 + ex2 + ex3;

            string help = intro + usage + portStatus + arguements + extraInfo;

            Console.WriteLine(help);
        }
    }
}
