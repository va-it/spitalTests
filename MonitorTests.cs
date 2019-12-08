using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using spital;

namespace spitalUnitTests
{
    [TestClass]
    public class MonitorTests
    {
        /// <summary>
        /// Tests that the function only returns active alarms
        /// </summary>
        [TestMethod]
        public void GetActiveAlarms_Test()
        {
            //Set up a monitor with ID 1 and save in database
            Monitor monitor = new Monitor();
            monitor.Id = 1;
            monitor.Active = true;
            monitor.Save();

            //Set up a module and save. Id is given by IDENTITY in database
            Module module = new Module();
            module.Name = "Module name";
            module.Icon = "icon";
            module.DefaultMin = 10F;
            module.DefaultMax = 20F;
            Nullable<int> insertedModuleId = module.Save();

            // Ensure that the ID is set by retrieving inserted record from DB
            module = new Module(insertedModuleId);


            //Set up a monitorModule and save
            MonitorModule monitorModule = new MonitorModule();
            monitorModule.Monitor = monitor;
            monitorModule.Module = module;
            monitorModule.AssignedMin = 11F;
            monitorModule.AssignedMax = 19F;
            Nullable<int> insertedMonitorModuleId =  monitorModule.Save();

            // Ensure that the ID is set by retrieving inserted record from DB
            monitorModule = MonitorModule.GetOne((int)insertedMonitorModuleId);

            
            //Create an active alarm for monitor 1
            Alarm activeAlarm = new Alarm(monitorModule, 10F);
            Nullable<int> insertedActiveAlarmID = activeAlarm.Save();
            activeAlarm.Id = (int)insertedActiveAlarmID;


            //Create an inactive alarm for monitor 1
            Alarm inactiveAlarm = new Alarm(monitorModule, 20F);
            Nullable<int> insertedInactiveAlarmID = inactiveAlarm.Save();
            inactiveAlarm.Id = (int)insertedInactiveAlarmID;

            // update alarm to mark as inactive
            inactiveAlarm.EndDateTime = DateTime.Now.AddDays(-1);
            inactiveAlarm.Update();


            // At this stage we have two alarms for the monitor with ID 1.
            // One of the is inactive because EndDateTime is set to yesterday.

            List<Alarm> retrievedActiveAlarms = new List<Alarm>();

            // Retrieve all active alarms for monitor 1
            retrievedActiveAlarms = monitor.GetActiveAlarms();

            // The retrievedActiveAlarms list should contain 1 record, 1 less than
            // the 2 that the alarmFixtures contains.
            Assert.AreEqual(1, retrievedActiveAlarms.Count);
        }
    }
}
