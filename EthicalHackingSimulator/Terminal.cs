using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    //For the classes which implement a console
    public interface ITerminal
    {
        void Terminal(); //Represents the "console"
        void PrintHelp(); //Help Menu
    }
}
