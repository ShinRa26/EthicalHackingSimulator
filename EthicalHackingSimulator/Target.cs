using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    /// <summary>
    /// Class to represent a Target.
    /// Holds all the necessar information such as Services, Port numbers, IP Address etc.
    /// </summary>
    public class Target
    {
        private const int MAX_IP = 255;
        private const int USABLE_PORTS = 34434;
        public TargetDB db;
        public string ipAddress { get; private set; }
        public bool isAlive { get; private set; }
        public Dictionary<int, string> portsAndServices {get; private set;}
        public Dictionary<int, string> portStatus { get; private set; }
        public bool remotelyConnected { get; set; }
        public MSExploit deployedExploit { get; set; } //MAYBE

        //Constructor
        public Target()
        {
            //Generates the IP Address
            this.ipAddress = GenerateIP();
            System.Threading.Thread.Sleep(25); //Without this, output is incorrect for a target (No idea why...)

            //Generates the ports and services for the Target.
            portsAndServices = new Dictionary<int, string>();
            GeneratePortsAndServices(portsAndServices);

            //generates the status of the ports for the Target
            portStatus = GeneratePortStatus(portsAndServices);

            //Specifies if the target is online/offline
            isAlive = InitiateAliveStatus();

            //Creates the Database for the target            
            db = InitiateDatabase(portsAndServices);

            //Initialised as False as no connection has been made
            remotelyConnected = false;

            //No exploit deployed to target
            deployedExploit = null; //MAYBE
        }

        //Generates an IP Address for the target.
        private string GenerateIP()
        {
            string ip = "";
            Random r = new Random();

            int firstSlot, secondSlot, thirdSlot, fourthSlot;
            firstSlot = r.Next(1, MAX_IP);
            secondSlot = r.Next(0, MAX_IP);
            thirdSlot = r.Next(0, MAX_IP);
            fourthSlot = r.Next(0, MAX_IP);

            ip = firstSlot + "." + secondSlot + "." + thirdSlot + "." + fourthSlot;

            return ip;
        }

        //Generates the services running on the target and their assigned port numbers
        private Dictionary<int, string> GeneratePortsAndServices(Dictionary<int, string> ps)
        {
            Random r = new Random();

            ps[21] = "FTP_Client";
            ps[22] = "SSH";
            ps[23] = "Telnet";
            ps[25] = "SMTP";
            ps[80] = "HTTP";

            int nextValue = r.Next(0, 4);

            //if Next Value is 0, only show HTTP(SSL)
            if (nextValue == 0)
                ps[443] = "HTTP_(SSL)";

            //If next Value is 1, only show HTTP Proxy
            else if (nextValue == 1)
                ps[8080] = "HTTP_Proxy";

            //If next value is 2, show both HTTP(SSL) and HTTP Proxy
            else if(nextValue == 2)
            {
                ps[443] = "HTTP_(SSL)";
                ps[8080] = "HTTP_Proxy";
            }

            //If next value is 3, then show none of these services [OMITTED]

            //Generates Random services on random port numbers, ranging from Port 100-34434
            HashSet<string> uniqueServices = SelectRandomServices();
            string[] uServices = new string[uniqueServices.Count];
            uniqueServices.CopyTo(uServices);

            for (int i = 0; i < uServices.Length; i++)
                ps[r.Next(100, USABLE_PORTS)] = uServices[i]; 

            return ps;
        }

        //Selects a set number of unique services
        private HashSet<string> SelectRandomServices()
        {
            List<string> serviceList = ListOfServices();            
            HashSet<string> uniqueServices = new HashSet<string>();            
            Random r = new Random();
            int maxServices = 3;

            for (int i = 0; i < maxServices; i++)
                uniqueServices.Add(serviceList[r.Next(0, serviceList.Count)]);

            return uniqueServices;
        }

        //A list of all extra services that can be running on a target
        private List<string> ListOfServices()
        {
            List<string> sl = new List<string>();

            sl.Add("MailChimp");
            sl.Add("MySQL_Database");
            sl.Add("Web_Server");
            sl.Add("File_Server");
            sl.Add("Drupal_System");
            sl.Add("Magneto");
            sl.Add("Apache3");
            sl.Add("AG_Antivirus");

            return sl;
        }

        //Generates the status of the ports (Open/Closed/Filtered)
        public Dictionary<int,string> GeneratePortStatus(Dictionary<int, string> ps)
        {
            Random r = new Random();
            Dictionary<int, string> pStatus = new Dictionary<int, string>();

            for(int i = 0; i < USABLE_PORTS; i++)
            {
                if (ps.ContainsKey(i))
                {
                    int chance = r.Next(0, 6);

                    if (chance == 0 || chance == 1 || chance == 2 || chance == 3)
                        pStatus[i] = "Open";
                    else if (chance == 4)
                        pStatus[i] = "Closed";
                    else if (chance == 5)
                        pStatus[i] = "Filtered";
                }
                else
                    pStatus[i] = "Closed";
            }

            return pStatus;
        }

        //Initiates the status of the Target (Online (Alive)/Offline(Dead))
        private bool InitiateAliveStatus()
        {
            bool status;
            Random r = new Random();

            int chance = r.Next(0, 4);

            if (chance == 0 || chance == 1 || chance == 2)
                status = true;
            else
                status = false;

            return status;
        }

        //Initialises a "database" for the target if certain services are present on the target.
        private TargetDB InitiateDatabase(Dictionary<int, string> ps)
        {
            //If the Target's services contain "MySQL_Database", create a TargetDB object for the Target.
            for(int i = 0; i < USABLE_PORTS; i++)
            {
                if (ps.ContainsKey(i) && ps[i] == "MySQL_Database")
                    return new TargetDB();                   
            }

            return null;
        }
    }
}
