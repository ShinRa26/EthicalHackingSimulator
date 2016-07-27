using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    public class Ping
    {
        private Target target;
        private bool isAlive;

        private bool invalidTarget;

        //Default Constructor
        public Ping(){}

        //Constructor for scanning the target
        public Ping(Target t)
        {
            try
            {
                this.target = t;
                this.isAlive = target.isAlive;

                invalidTarget = false;
            }
            catch(Exception)
            {
                invalidTarget = true;
            }
        }

        //Pings the target to determine if it is alive (Online)
        public void Scan()
        {
            if (invalidTarget)
                Console.WriteLine("That is not a valid target.\n");

            else if (isAlive)
                AlivePing();

            else
                DeadPing();
        }

        //Output if the target is alive
        private void AlivePing()
        {
            Console.WriteLine("\nPinging {0} with 32 bytes of data:",target.ipAddress);

            //Displays response 4 times
            for(int i = 0; i < 4; i++)
            {
                Console.WriteLine("Reply from {0}: bytes=32 TTL=64", target.ipAddress);
                System.Threading.Thread.Sleep(750);
            }

            //Prints out the statistics
            Console.WriteLine("\nPing statistics for {0}:", target.ipAddress);
            Console.WriteLine("\tPackets: Sent = 4, Received = 4, Lost = 0 (0% Loss)\n");
        }

        //Output if the target is dead (Offline)
        private void DeadPing()
        {
            Console.WriteLine("\nPinging {0} with 32 bytes of data:", target.ipAddress);
            System.Threading.Thread.Sleep(1500);

            //Displays no response message 4 times
            for(int i = 0; i < 4; i++)
            {
                Console.WriteLine("Request timed out.");
                System.Threading.Thread.Sleep(1500);
            }

            //Prints out the statistics
            Console.WriteLine("\nPing statistics for {0}:", target.ipAddress);
            Console.WriteLine("\tPackets: Sent = 4, Received = 0, Lost = 4 (100% Loss)\n");
        }

        //Prints the Help menu for the Ping module out to the screen
        public void Help()
        {
            string p1 = "\nPing: Sends a signal to the target and awaits a reply.\nIf the 'Target' is online, then a reply is sent. If not, the request will time out.";

            string pingUsage = "\n\nUsage: ";
            string pUsage1 = "ping {target ip}\n\n";

            string exInfo = "Note:\nThis is not an actual ping tool, it is only an emulation of what a ping tool on most computers will do.\n";
            string exInfo1 = "Most computers come pre-installed with a Ping tool. Check your OS documentation for more information.\n";

            Console.WriteLine(p1 + pingUsage + pUsage1 + exInfo + exInfo1);
        }
    }
}
