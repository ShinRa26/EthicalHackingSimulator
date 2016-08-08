using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    public class ExitScenarios
    {
        private string exName { get; set; }
        private bool exRootkit { get; set; }

        //Default Constructor
        public ExitScenarios() {}

        //For processing all exploits other than SQL Injection
        public ExitScenarios(string exName, bool exRootkit)
        {
            this.exName = exName;
            this.exRootkit = exRootkit;
        }

        public void DisplayScenario()
        {
            //FILL IN
        }

        //Buffer Overflow Ending
        private void BufferOverflowScenario(bool rootkit)
        {
            if(rootkit)
            {
                //Say well done and shit
            }
            else
            {
                //No rootkit, boohoo.
            }
        }

        //Heap Overflow Ending
        private void HeapOverflowScenario(bool rootkit)
        {
            if(rootkit)
            {
                //BLAH
            }
            else
            {
                //BOOHOO
            }
        }

        //Reverse Shell Ending
        private void ReverseShellScenario(bool rootkit)
        {
            if(rootkit)
            {
                //BLAH
            }
            else
            {
                //BOOHOO
            }
        }

        //File Upload Ending
        private void FileUploadScenario(bool rootkit)
        {
            if(rootkit)
            {
                //BLAH
            }
            else
            {
                //BOOHOO
            }
        }

        //SQL Ending (Weird...)
        public void SQLInjectionScenario()
        {
            //figure this out...
        }
    }
}
