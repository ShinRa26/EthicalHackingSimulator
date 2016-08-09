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
                        string rk1 = "You have chosen to install a rootkit along with your exploit. This allows you to maintain access to the target's computer, even after they restart it.";
                        string rk2 = "Most computer exploits exist in temporary memory which is lost when the computer restarts. By installing a rootkit, your exploit will continue to live on until detected and detroyed.";
                        string rk3 = "Maintaining access to a compromised system is a key part when attacking a target. In terms of ethical hacking, this can highlight a more crucial flaw in the target's system which can be addressed.";
                        string rk4 = "In the case of this virus upload, the system is rendered inactive due to key file deletion. The system may not be able to recover so a rootkit is not necessary!";
                        string rk5 = "\nWell Done! You're exploit has destroyed the system and a rootkit is not necessary!\n";

                        finalOutput = baseOutput + rk1 + rk2 + rk3 + rk4 + rk5;
                    }
                    else
                    {
                        string rk1 = "You have chosen to install a rootkit along with your exploit. This allows you to maintain access to the target's computer, even after they restart it.";
                        string rk2 = "Most computer exploits exist in temporary memory which is lost when the computer restarts. By installing a rootkit, your exploit will continue to live on until detected and detroyed.";
                        string rk3 = "Maintaining access to a compromised system is a key part when attacking a target. In terms of ethical hacking, this can highlight a more crucial flaw in the target's system which can be addressed.";
                        string rk4 = "In the case of this file type, the executable will persist on the target's machine until it is identified and removed so a rootkit is not entirely necessary but is good practice!";
                        string rk5 = "\nWell Done! Your exploit will remain until removed but there wil always be a way back in!\n";

                        finalOutput = baseOutput + rk1 + rk2 + rk3 + rk4 + rk5;
                    }
                }
                else
                {
                    if(fileType == "Virus" || fileType == "virus")
                    {
                        string noRk1 = "You have not chosen to install a rootkit along with your exploit. Rootkits allow you maintain access to a target's computer, even after it is restarted.";
                        string noRk2 = "Most computer exploits exist in temporary memory which is lost when the computer restarts. By installing a rootkit, your exploit will continue to live on until detected and detroyed.";
                        string noRk3 = "Maintaining access to a compromised system is a key part when attacking a target. In terms of ethical hacking, this can highlight a more crucial flaw in the target's system which can be addressed.";
                        string noRk4 = "In the case of this virus upload, a rootkit is not necessary as the system is rendered useless but looking for a way to install a rootkit can help highlight more crucial flaws in a target system.";
                        string noRk5 = "\nWell Done! Your exploit has destroyed the system but it is always good practice to look for methods to maintain access to a system, such as a rootkit!\n";

                        finalOutput = baseOutput = noRk1 + noRk2 + noRk3 + noRk4 + noRk5;
                    }
                    else
                    {
                        string noRk1 = "You have not chosen to install a rootkit along with your exploit. Rootkits allow you maintain access to a target's computer, even after it is restarted.";
                        string noRk2 = "Most computer exploits exist in temporary memory which is lost when the computer restarts. By installing a rootkit, your exploit will continue to live on until detected and detroyed.";
                        string noRk3 = "Maintaining access to a compromised system is a key part when attacking a target. In terms of ethical hacking, this can highlight a more crucial flaw in the target's system which can be addressed.";
                        string noRk4 = "In the case of this file type, the executable will persist on the target's machine until it is identified and removed so a rootkit is not entirely necessary but it's good practice!";
                        string noRk5 = "\nWell Done! Your exploit will remain until removed but unfortunately, once it's removed there will be no way back into the system!\n";

                        finalOutput = baseOutput + noRk1 + noRk2 + noRk3 + noRk4 + noRk5;
                    }
                }

                Console.WriteLine(finalOutput);
            }           
        }

        //SQL Injection scenario (USED SEPARATELY)
        public void SQLInjectionScenario()
        {
            string out1 = "You have chosen to exploit the target's system by the means of an SQL Injection. This is a unique type of exploit that obtains information from insecure SQL databases.";
            string out2 = "SQL Injections pass SQL commands to the database instead of the information it is expecting i.e. a Username/password combination. Insecure databases will not escape this SQL command and pass it directly into the database where it is executed.";
            string out3 = "Secure databases will take the input as a string of text and parse it at later stages to obtain the necessary information. These are called Prepared Statements which are necessary to prevent direct SQL comamnd execution.";
            string out4 = "In the case of this target's database, it does not use prepared statements so direct SQL commands can be sent to the database and the requested information returned.";
            string out5 = "This exploit manages to obtain a list of username and passwords (in plain text which is another security issue) that can be used to log in to the target's machine through a remote connection.";
            string out6 = "By using this information, you have successfully managed to log in to Telnet and gained complete access to the target's machine!";
            string out7 = "No rootkit is necessary as you already have a secure method of gaining access. Access will only be revoked if the target changes the username/passwords on the machine!";
            string out8 = "\nWell Done! Your exploit has lead you to have complete control of the target's machine!\n";

            string output = out1 + out2 + out3 + out4 + out5 + out6 + out7 + out8;

            Console.WriteLine(output);
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
            string base1 = "You have chosen to exploit the target's system by the means of an Arbitrary File Upload using a virus file.";
            string base2 = "A virus is a malicious type of file that attaches itself to a host file in order to execute it's payload. In this case, it's attached to a small executable file that will execute after a period of time - a timebomb style virus.";
            string base3 = "This particular virus escalates system priveleges to allow access to key system files. Once access is gained, it starts deleting key files crucial to the system's operating system.";
            string base4 = "Once these files are deleted, this renders the system unusable until a system repair can identify and reinstate the missing system files if possible.";

            string baseOutput = base1 + base2 + base3 + base4;

            return baseOutput;
        }

        //Text for if a Worm file was used in the Arbitrary File Upload exploit
        private string AFU_WormType()
        {
            string base1 = "You have chosen to exploit the target's system by the means of an Arbitrary File Upload using a worm file.";
            string base2 = "A worm is a standalone malicious program that spreads itself through network communications such as low level network protocols or high level communication types such as Email.";
            string base3 = "In the case of this worm, it's payload is to connect to a remote download server and download files to bottleneck the target's network bandwith. It thenn obtains the target's email list and mass emails itself to other members on the network, further choking the bandwith.";
            string base4 = "This will casue a dramatic slowdown in network speed, making normal work routine difficult and costing the target hours of lost time!";

            string baseOutput = base1 + base2 + base3 + base4;

            return baseOutput;
        }

        //Text for if a Trojan file was used in the Arbitrary File Upload exploit
        private string AFU_TrojanType()
        {
            string base1 = "You have chosen to explit the target's system by the means of an Arbitrary File Upload using a trojan file.";
            string base2 = "A trojan is a malicious program that disguises itself as a harmless or innocuous program. Once the user activates the file, the payload will execute it's payload.";
            string base3 = "In the case of this trojan, it's payload will log the keystrokes on the target's machine and relay them back to the attacker. This can yield valuable and sensitive information!";
            string base4 = "Once the keystrokes have been logged, the attacker can then read the keystrokes to obtain the necessary information they want!";

            string baseOutput = base1 + base2 + base3 + base4;

            return baseOutput;
        }
    }
}
