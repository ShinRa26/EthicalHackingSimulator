﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    /// <summary>
    /// Main class.
    /// Launches the program
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        { 
            //Starts the program           
            var runApp = new RunApplication();
            runApp.StartApp();                        
        }
    }
}
