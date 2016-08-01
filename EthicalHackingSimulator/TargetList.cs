using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    public class TargetList
    {
        public Target[] targetList { get; set; }

        public TargetList(int total)
        {
            targetList = new Target[total];

            for (int i = 0; i < targetList.Length; i++)
                targetList[i] = new Target();        
        }

        //Prints the list of IP Addresses assiciated with the Targets
        public void PrintList()
        {
            Console.WriteLine();
            Console.WriteLine("List of available targets: ");
            for(int i = 0; i < targetList.Length; i++)
                Console.WriteLine("Target " + (i + 1) + ": " + targetList[i].ipAddress);

            Console.WriteLine();
        }

        //Finds a specific target in the list
        public Target FindTarget(string ip)
        {
            for (int i = 0; i < targetList.Length; i++)
            {
                if (ip == targetList[i].ipAddress)
                    return targetList[i];

            }
            return null;
        }
    }
}
