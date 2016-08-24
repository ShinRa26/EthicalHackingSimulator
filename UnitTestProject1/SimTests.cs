using System;
using System.Collections.Generic;
using EthicalHackingSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EthicalHackingSimUnitTests
{
    [TestClass]
    public class SimTests
    {
        [TestMethod]
        public void TargetIsNotNull()
        {
            Target t = new Target();
            Assert.IsNotNull(t, "Target is null");
        }

        [TestMethod]
        public void NumberOfTargetsCreatedIsCorrect()
        {
            TargetList t = new TargetList(5);
            Assert.AreEqual(5, t.targetList.Length);            
        }

        [TestMethod]
        public void NoNullTargets()
        {
            TargetList t = new TargetList(12);
            Target[] list = t.targetList;

            for (int i = 0; i < list.Length; i++)
                Assert.IsNotNull(list[i], "Null target found");
        }

        [TestMethod]
        public void TargetDBCreatedWhenNeeded()
        {
            Target t = new Target();
            var d = t.portsAndServices;
            bool exit = false;
            foreach(KeyValuePair<int, string> kvp in d)
            {
                if (kvp.Value == "MySQL_Database")
                {
                    Assert.IsNotNull(t.db, "No Target DB created");
                    exit = true;
                }
                else
                    continue;                
            }

            if (!exit)
                Assert.Fail();           
        }
    }
}
