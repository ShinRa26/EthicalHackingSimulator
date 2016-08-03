using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthicalHackingSimulator
{
    public class TargetDB
    {
        //TODO Maybe replace the Lists with the Arrays
        public Dictionary<string, string> table { get; private set; }

        public TargetDB()
        {
            table = InitiateTable();
        }

        //Creates the Database table
        private Dictionary<string, string> InitiateTable()
        {
            var t = new Dictionary<string, string>();
            var usernames = Usernames();
            var passwords = Passwords();

            for (int i = 0; i < usernames.Count; i++)            
                t[usernames[i]] = passwords[i];                    

            return t;
        }

        //Sets up the List of Usernames to be used in the Database Table
        //Could be replaced by UsernameCollection()...
        private List<string> Usernames()
        {
            var uNames = new List<string>();
            string[] uCollection = UsernameCollection();

            for (int i = 0; i < uCollection.Length; i++)
                uNames.Add(uCollection[i]);

            return uNames;
        }

        //Sets up the list of passwords to be used in the Database Table
        //Could be replaced by PasswordCollection()...
        private List<string> Passwords()
        {
            var pWords = new List<string>();
            string[] pCollection = PasswordCollection();

            for (int i = 0; i < pCollection.Length; i++)
                pWords.Add(pCollection[i]);

            return pWords;
        }

        //A collection of usernames. One of the various arrays will be chosen to be returned.
        private string[] UsernameCollection()
        {
            Random r = new Random();

            string[] choice1 =
            {
                "admin",
                "starlord84",
                "clannister",
                "michaeljwoods",
                "elisande"
            };

            string[] choice2 =
            {
                "rbradley",
                "jstark",
                "strathmar72",
                "jr58216",
                "emilystrange"
            };

            string[] choice3 =
            {
                "cjones87",
                "robertsmith",
                "wolfstar44",
                "sholmes77",
                "jenny7489"
            };

            string[] choice4 =
            {
                "sarahhill",
                "jwatson",
                "dstormborn",
                "jmoriarty",
                "davidsmith"
            };

            //Randomly selects one of the above selections and returns it.
            int chance = r.Next(0, 4);
            System.Threading.Thread.Sleep(25); //Necessary to fix output

            //Return selected array based on value of chance
            if (chance == 0)
                return choice1;
            else if (chance == 1)
                return choice2;
            else if (chance == 2)
                return choice3;
            else
                return choice4;
        }

        //A collection of passwords. One of the various arrays will be chosen to be returned.
        private string[] PasswordCollection()
        {
            Random r = new Random();

            string[] choice1 =
            {
                "SeCuReP4sSwOrD",
                "MJackson1984",
                "123456",
                "wordpass56",
                "T1MEL4PS3"
            };

            string[] choice2 =
            {
                "kfx9832cfr",
                "GWarriors1",
                "password123456",
                "ihatemyjob",
                "tartanarmy2022"
            };

            string[] choice3 =
            {
                "jcb5976gsc",
                "password",
                "slayer2010",
                "C3l3st14lGu4rd",
                "qwerty"
            };

            string[] choice4 =
            {
                "shadowPr1est92",
                "football",
                "69696970",
                "abc123",
                "SPARKUGGZ74"
            };

            //Randomly selects one of the above selections and returns it.
            int chance = r.Next(0, 4);
            System.Threading.Thread.Sleep(25); //Necessary to fix output

            //Returns the selected array based on the value of chance
            if (chance == 0)
                return choice1;
            else if (chance == 1)
                return choice2;
            else if (chance == 2)
                return choice3;
            else
                return choice4;            
        }
    }
}
