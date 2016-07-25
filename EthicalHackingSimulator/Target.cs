using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    public class Target
    {
        private const int MAX_IP = 255;
        private const int USABLE_PORTS = 34434;
        public string ipAddress { get; set; }
        public bool isAlive { get; set; }
        public Dictionary<int, string> portsAndServices {get; set;}
        public Dictionary<int, string> portStatus { get; set; }

        public Target()
        {
            this.ipAddress = GenerateIP();
            System.Threading.Thread.Sleep(25);

            portsAndServices = new Dictionary<int, string>();
            GeneratePortsAndServices(portsAndServices);

            portStatus = GeneratePortStatus(portsAndServices);

            isAlive = InitiateAliveStatus();
        }

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

        public Dictionary<int,string> GeneratePortStatus(Dictionary<int, string> ps)
        {
            Random r = new Random();
            Dictionary<int, string> pStatus = new Dictionary<int, string>();

            for(int i = 0; i < USABLE_PORTS; i++)
            {
                if (ps.ContainsKey(i))
                {
                    int chance = r.Next(0, 3);

                    if (chance == 0)
                        pStatus[i] = "Open";
                    else if (chance == 1)
                        pStatus[i] = "Closed";
                    else if (chance == 2)
                        pStatus[i] = "Filtered";
                }
                else
                    pStatus[i] = "Closed";
            }

            return ps;
        }

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
    }
}
