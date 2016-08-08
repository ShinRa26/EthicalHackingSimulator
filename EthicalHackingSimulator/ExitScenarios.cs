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
            if (exName == "Buffer_Overflow")
            {
                BufferOverflowScenario(exRootkit);
            }
            else if(exName == "Arbitrary_File_Upload")
            {
                FileUploadScenario(exRootkit);
            }
            else if(exName == "DoS_Heap_Overflow")
            {
                HeapOverflowScenario(exRootkit);
            }
            else if(exName == "Reverse_TCP_Shell")
            {
                ReverseShellScenario(exRootkit);
            }
            else
            {
                Console.WriteLine("Error in processing Exploit Name.\n");
                return;
            }
        }

        //Buffer Overflow Ending
        private void BufferOverflowScenario(bool rootkit)
        {
            string base1 = "You have chosen to exploit the target by using a Buffer Overflow. This takes advantage of the Stack, an area of memory utilised by the program, to point the program to your malicious shellcode.\n";
            string base2 = "By corrupting the memory locations in the stack with No-Op machine code - code which means do nothing - this causes the program to continuely execute thee commands until a \"Jump\" instruction is met.\n";
            string base3 = "By guessing the location of the return address in the code, the jump instruction \"jumped\" over this and continued to execute the No Op codes until it met yet another Jump instruction.\n";
            string base4 = "The final jump instruction causes the program to jump right into the malicious shellcode that was created. Depending on what the shellcode does, this could have disasterous consequences!\n";
            string base5 = "In this case, you've caused the program to crash entirely, rendering it useless.";

            string baseOutput = base1 + base2 + base3 + base4 + base5;

            string finalOutput = "";

            if(rootkit)
            {
                string rootkitText = RootkitChosen();
                finalOutput = baseOutput + rootkitText;
            }
            else
            {
                string noRootkitText = NoRootkitChosen();
                finalOutput = baseOutput + noRootkitText;
            }

            Console.WriteLine(finalOutput);
        }

        //Heap Overflow Ending
        private void HeapOverflowScenario(bool rootkit)
        {
            string base1 = "";

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

        //SQL Injection scenario (USED SEPARATELY)
        public void SQLInjectionScenario()
        {
            //figure this out...
        }

        //Text for if the user has chosen to install a rootkit
        private string RootkitChosen()
        {
            string rk1 = "You have chosen to install a rootkit along with your exploit. This allows you to maintain access to the target's computer, even after they restart it.";
            string rk2 = "Most computer exploits exist in temporary memory which is lost when the computer restarts. By installing a rootkit, your exploit will continue to live on until detected and detroyed.";
            string rk3 = "Maintaining access to a compromised system is a key part when attacking a target. In terms of ethical hacking, this can highlight a more crucial flaw in the target's system which can be addressed.";
            string rk4 = "\nWell done! You're exploit has successfully lived on in the system!\n";

            string rootkitText = rk1 + rk2 + rk3 + rk4;

            return rootkitText;
        }

        //Text for if the user has not chosen to install a rootkit
        private string NoRootkitChosen()
        {
            string noRk1 = "You have not chosen to install a rootkit along with your exploit. Rootkits allow you maintain access to a target's computer, even after it is restarted.";
            string noRk2 = "Most computer exploits exist in temporary memory which is lost when the computer restarts. By installing a rootkit, your exploit will continue to live on until detected and detroyed.";
            string noRk3 = "Maintaining access to a compromised system is a key part when attacking a target. In terms of ethical hacking, this can highlight a more crucial flaw in the target's system which can be addressed.";
            string noRk4 = "Unfortunately, without a rootkit, your exploit will be lost when the target restarts their system.";
            string noRk5 = "\nWell done! You managed to exploit the system! But unfortunately for you, the exploit is lost!\n";

            string noRootkit = noRk1 + noRk2 + noRk3 + noRk4 + noRk5;

            return noRootkit;
        }
    }
}
