﻿using System;
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

        //Prints the intro text for the application
        public void PrintIntro()
        {
            targets.PrintList(); //TEMP
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
    }
}
