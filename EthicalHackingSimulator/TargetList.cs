﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    /// <summary>
    /// Class to hold an array of Target objects.
    /// Sent to the Application class.
    /// </summary>
    public class TargetList
    {
        public Target[] targetList { get; set; }

        //Constructor - total is the total number of target objects to create
        public TargetList(int total)
        {
            targetList = new Target[total];

            for (int i = 0; i < targetList.Length; i++)
                targetList[i] = new Target();        
        }

        //Prints the list of IP Addresses assiciated with the Targets.
        public void PrintList()
        {
            Console.WriteLine();
            Console.WriteLine("List of available target ip addresses: ");
            for(int i = 0; i < targetList.Length; i++)
                Console.WriteLine(targetList[i].ipAddress);

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
