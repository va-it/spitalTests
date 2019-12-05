using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using spital;

namespace spitalUnitTests
{
    [TestClass]
    public class ModuleTests
    {
        [TestMethod]
        public void GetOne_Test()
        {
            Module module = new Module
            {
                Name = "Module name",
                Icon = "icon",
                DefaultMin = 1.5F,
                DefaultMax = 10.5F
            };

            Nullable<int> insertedID = module.Save();

            Module retrievedModule = new Module();

            retrievedModule = Module.GetOne(insertedID);

            /* 
             * Cannot assert that the two objects are the same because
             * the comparison is done by reference. Every property
             * must be checked to ensure the two objects are the same
             */
            Assert.AreEqual(retrievedModule.Id, insertedID);
            Assert.AreEqual(retrievedModule.Name, module.Name);
            Assert.AreEqual(retrievedModule.Icon, module.Icon);
            Assert.AreEqual(retrievedModule.DefaultMin, module.DefaultMin);
            Assert.AreEqual(retrievedModule.DefaultMax, module.DefaultMax);
        }
    }
}
