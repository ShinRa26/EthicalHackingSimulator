using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    public class Application
    {
        public TargetList targets;
        private Target[] t;
        public ExploitDB exploitDatabase;

        public Application()
        {
            targets = new TargetList(5);
            t = targets.targetList;

            exploitDatabase = new ExploitDB();
        }

        //Prints the intro text for the application.
        public void PrintIntro()
        {
            string title = "\t\t\tWelcome to the Ethical Hacking Simulator!\n\n";
            string intro1 = "You are an employee of a security firm and have been contracted by a firm to test their security.\n";
            string intro2 = "They have asked you to use your skills as a penetration tester to find potential weaknesses in their security.\n";
            string intro3 = "After a few months of reconaissance, you have identified "+ t.Length + " potential targets within the firm.\n";
            string intro4 = "Your task is find, create, and deploy an exploit onto a target. This will be enough for the firm to address any security flaws you have found.\n\n";

            string ipAddresses = IntroTargets();

            string introFull = title + intro1 + intro2 + intro3 + intro4 + ipAddresses;

            Console.WriteLine(introFull);
            
        }

        //Prints the IP Addresses of the available Targets
        public void PrintTargets()
        {
            targets.PrintList();
        }

        //Returns the Target with the matching IP Address
        public Target FindTarget(string ip)
        {
            for(int i = 0; i < t.Length; i++)
            {
                if (ip == t[i].ipAddress)
                    return t[i];
                
            }
            return null;
        }

        //List of targets for Intro
        private string IntroTargets()
        {
            string targets = "Target IP Addresses:\n";

            for(int i = 0; i < t.Length; i++)
            {
                targets += (t[i].ipAddress + "\n");
            }

            targets += "\n";

            return targets;
        }
    }
}
