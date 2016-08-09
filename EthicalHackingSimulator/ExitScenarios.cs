using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    public class ExitScenarios
    {
        private MSExploit exploit { get; set; }
        private string exName { get; set; }
        private bool exRootkit { get; set; }

        //Default Constructor
        public ExitScenarios() {}

        //For processing all exploits other than SQL Injection
        public ExitScenarios(MSExploit exploit)
        {
            this.exploit = exploit;
            this.exName = exploit.name;
            this.exRootkit = exploit.optionalRootkit;
        }

        //Displays the appropriate scenario depending on which type of exploit was used
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
            string base1 = "You have chosen to exploit the target by using a Heap Overflow, which in this case, will cause a Denial of Service - taking the target service offline.";
            string base2 = "Heap Overflow is a type of Buffer Overflow but differs from the common Stack Buffer Overflow. Heap overflows utilise the heap memory of the program - dynamically allocated memory to be used for program data.";
            string base3 = "By corrupting heap data, such as overwriting dynamic memory allocation, the exploit can use the exchange of memory pointers to overwrite a program function pointer.";
            string base4 = "This can cause the program to crash and stop responding, which is wat has happened in this case!";

            string baseOutput = base1 + base2 + base3 + base4;

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

        //Reverse Shell Ending
        private void ReverseShellScenario(bool rootkit)
        {
            string base1 = "You have chosen to exploit the target by using a Reverse TCP Shell. This allows for a remote connection between the attacker's machine and the target's machine.";
            string base2 = "A reverse shell is when a script on the target's machine initiates a connection to the attacker's machine. The attacker's machine listens for the incoming connection on a specific port.";
            string base3 = "Once a connection has been established, the attacker can input commands that will be executed on the target's machine. Commands such as changing directories, downloading malicious files, or deleting key files.";
            string base4 = "Another type of shell is a Bind Shell which is similar to a Reverse Shell but the connection is initiated by the attacker rather than the target.";
            string base5 = "In this case, you have achieved a remote connection to the target so you essentially have free reign over their machine!";

            string baseOutput = base1 + base2 + base3 + base4 + base5;

            string finalOutput = "";

            //Unique as the reverse Shell can be a persistent executable
            if(rootkit)
            {
                string rk1 = "You have chosen to install a rootkit along with your exploit. This allows you to maintain access to the target's computer, even after they restart it.";
                string rk2 = "Most computer exploits exist in temporary memory which is lost when the computer restarts. By installing a rootkit, your exploit will continue to live on until detected and detroyed.";
                string rk3 = "Maintaining access to a compromised system is a key part when attacking a target. In terms of ethical hacking, this can highlight a more crucial flaw in the target's system which can be addressed.";
                string rk4 = "In the case of this reverse shell, a rootkit is not necessary as the executable script will persist on the target's machine until it is deleted.";
                string rk5 = "\nRegardless, you have successfully allowed for your exploit to live on in the target's system!\n";

                string rootkitText = rk1 + rk2 + rk3 + rk4 + rk5;

                finalOutput = baseOutput + rootkitText;
            }
            else
            {
                string noRk1 = "You have not chosen to install a rootkit along with your exploit. Rootkits allow you maintain access to a target's computer, even after it is restarted.";
                string noRk2 = "Most computer exploits exist in temporary memory which is lost when the computer restarts. By installing a rootkit, your exploit will continue to live on until detected and detroyed.";
                string noRk3 = "Maintaining access to a compromised system is a key part when attacking a target. In terms of ethical hacking, this can highlight a more crucial flaw in the target's system which can be addressed.";
                string noRk4 = "In the case of this reverse shell, a rootkit is not necessary as the executable script will persist on the target's machine until it is deleted.";
                string noRk5 = "\nWell Done! Your exploit will live on in the target's system! But in future, rootkits are a good option to consider for temporary exploits!\n";

                string noRootkitText = noRk1 + noRk2 + noRk3 + noRk4 + noRk5;

                finalOutput = baseOutput + noRootkitText;
            }

            Console.WriteLine(finalOutput);
        }

        //File Upload Ending
        private void FileUploadScenario(bool rootkit)
        {
            //If the exploit is of the type MSExploit_File_Upload, then execute.
            if (exploit.GetType() == typeof(MSExploit_File_Upload))
            {                
                var fileUpload = (MSExploit_File_Upload)exploit;

                string fileType = fileUpload.type;
                string baseOutput = "";
                string finalOutput = "";

                //Parses the file type used in the exploit
                if(fileType == "Virus" || fileType == "virus")
                {
                    baseOutput = AFU_VirusType();
                }
                else if(fileType == "Worm" || fileType == "worm")
                {
                    baseOutput = AFU_WormType();
                }
                else if(fileType == "Trojan" || fileType == "torjan")
                {
                    baseOutput = AFU_TrojanType();
                }
                else
                {
                    baseOutput = "Error in parsing filetype. You fucked up, Graham...";
                    return;
                }

                //Displays the output dependent on if a rootkit was installed or not
                if (rootkit)
                {
                    if(fileType == "Virus" || fileType == "virus")
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    if(fileType == "Virus" || fileType == "virus")
                    {

                    }
                    else
                    {

                    }
                }
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

        //Text for if a Virus file was used in the Arbitrary File Upload Exploit
        private string AFU_VirusType()
        {


            return "";
        }

        //Text for if a Worm fle was used in the Arbitrary File Upload exploit
        private string AFU_WormType()
        {


            return "";
        }

        //Text for if a Trojan file was used in the Arbitrary File Upload exploit
        private string AFU_TrojanType()
        {


            return "";
        }
    }
}
