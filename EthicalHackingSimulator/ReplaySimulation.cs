using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    public class ReplaySimulation
    {        
        public ReplaySimulation()
        {
            //Empty
        }

        //Restarting the simulation
        public void RestartSimulation()
        {
            Console.Write("Do you wish to restart the simulation? All progress will be lost and new targets/exploits will be created. (y/n) ");
            string response = Console.ReadLine();

            if(response == "y" || response == "Y")
            {
                Console.Clear();
                var restartSim = new RunApplication();                
                restartSim.StartApp();
            }
            else if(response == "n" || response == "N")
            {
                return;
            }
            else
            {
                Console.WriteLine("Enter y or n for your response...");
                return;
            }
        }

        //Replay the simulation (Appears at the end)
        public void RerunSimulation()
        {
            Console.Write("Do you wish to replay the simulation with new targets and exploits? (y/n) ");
            string response = Console.ReadLine();

            if(response == "Y" || response == "y")
            {
                Console.Clear();
                var replaySim = new RunApplication();
                replaySim.StartApp();
            }
            else if(response == "n" || response == "N")
            {
                Console.WriteLine("Quitting Application...");
                System.Threading.Thread.Sleep(2000);
                Environment.Exit(0);
            }
        }
    }
}
