using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using spital;

namespace spitalUnitTests
{
    [TestClass]
    public class ModuleTests
    {
        /// <summary>
        /// Tests that the overloaded Constructor correctly instantiates
        /// object with values from database
        /// </summary>
        [TestMethod]
        public void Module_Test()
        {
            Module module = new Module
            {
                Name = "Module name",
                Icon = "icon",
                DefaultMin = 1.5F,
                DefaultMax = 10.5F
            };

            Nullable<int> insertedID = module.Save();

            Module instantiatedModule = new Module(insertedID);

            /* 
             * Cannot assert that the two objects are the same because
             * the comparison is done by reference. Every property
             * must be checked to ensure the two objects are the same
             */
            Assert.AreEqual(instantiatedModule.Id, insertedID);
            Assert.AreEqual(instantiatedModule.Name, module.Name);
            Assert.AreEqual(instantiatedModule.Icon, module.Icon);
            Assert.AreEqual(instantiatedModule.DefaultMin, module.DefaultMin);
            Assert.AreEqual(instantiatedModule.DefaultMax, module.DefaultMax);
        }
    }
}
