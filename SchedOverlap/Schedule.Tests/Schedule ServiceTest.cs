using NUnit.Framework;
using System.Collections.Generic;
using Schedule.Services;
using System.IO;
using System;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace Schedule.UnitTests.Services
{
    [TestFixture]
    public class Schedule_Test
    {
        private readonly ScheduleSerivce _scheduleService;
        // sample data for same employee id 1 schedule for 27 June 2018 09:00am - 17:00pm
        public string json1 = @"{
            ""Id"": 1,
            ""Employee"": 1,
            ""Department"": 1,
            ""StartTime"": 1530054000,
            ""EndTime"": 1530082800
            }";

        // sample data for same employee id 1 schedule for 27 June 2018 09:00am - 17:00pm
        public string json2 = @"{
            ""Id"": 1,
            ""Employee"": 1,
            ""Department"": 1,
            ""StartTime"": 1530054000,
            ""EndTime"": 1530082800

            }";

        // sample data for same employee id 1 schedule for 27 June 2018 08:30am - 12:00pm
        public string json4 = @"{
            ""Id"": 1,
            ""Employee"": 1,
            ""Department"": 1,
            ""StartTime"": 1530052200,
            ""EndTime"": 1530064800
            }";

        // sample data for same employee id 1 schedule for 27 June 2018 12:30am - 15:00pm
        public string json5 = @"{
            ""Id"": 1,
            ""Employee"": 1,
            ""Department"": 1,
            ""StartTime"": 1530066600,
            ""EndTime"": 1530075600
            }";

        // sample data for same employee id 2 schedule for 27 June 2018 09:00 - 17:00
        public string json3 = @"{
            ""Id"": 2,
            ""Employee"": 1,
            ""Department"": 1,
            ""StartTime"": 1530054000,
            ""EndTime"": 1530082800
            }";

        public Schedule_Test()
        {

            _scheduleService = new ScheduleSerivce();

        }

        //Check the insert object role with json file
        [Test]
        public void checkSetTimeSchedule()
        {
            //Check the get schedule function converting json to object 
            TimeSchedule roleList = _scheduleService.getSchedule(json1);

            //This is to check if the object is not empty or any error return from the function will make object into null
            Assert.IsNotNull(roleList, "Unable to parse json 1 to time schedule object");

            //Check the get schedule function converting json to object 
            roleList = _scheduleService.getSchedule(json2);

            //This is to check if the object is not empty or any error return from the function will make object into null
            Assert.IsNotNull(roleList, "Unable to parse json 2 to time schedule object");

            //Check the get schedule function converting json to object 
            roleList = _scheduleService.getSchedule(json3);

            //This is to check if the object is not empty or any error return from the function will make object into null
            Assert.IsNotNull(roleList, "Unable to parse json 3 to time schedule object");

            //Check the get schedule function converting json to object 
            roleList = _scheduleService.getSchedule(json4);

            //This is to check if the object is not empty or any error return from the function will make object into null
            Assert.IsNotNull(roleList, "Unable to parse json 4 to time schedule object");

            //Check the get schedule function converting json to object 
            roleList = _scheduleService.getSchedule(json5);

            //This is to check if the object is not empty or any error return from the function will make object into null
            Assert.IsNotNull(roleList, "Unable to parse json 5 to time schedule object");
        }

        // //Check the insert object user with json file 
        // [Test]
        // public void checkSetUsers()
        // {
        //     List<User> userList =  _userService.setUsers(Path.GetFullPath(@"users.json"));   

        //     //This is to check if the object is not empty or any error return from the function will make object into null
        //     Assert.IsNotEmpty(userList, "Unable to insert users.json to user object");           

        // }

        //Check the value for the overlapping
        [Test]
        public void validateResult1()
        {
            //Initialize the user list object and timeschedule object 
            TimeSchedule ts1 = _scheduleService.getSchedule(json1);
            TimeSchedule ts2 = _scheduleService.getSchedule(json2);
            TimeSchedule ts3 = _scheduleService.getSchedule(json3);
            TimeSchedule ts4 = _scheduleService.getSchedule(json4);
            TimeSchedule ts5 = _scheduleService.getSchedule(json5);

            //Run run the subordinate method to return value
            //same timing with same employee id
            bool result = _scheduleService.isOverlapping(ts1, ts2);

            //In case return error user allowed to see what is the return error 
            Console.WriteLine(result.ToString());

            //This is to check if return valued is return as expected for subordinate 3 
            Assert.IsTrue(result, "Same timing with same employee id - Overlapping - Test failed");

            //same timing but different employee id 
            result = _scheduleService.isOverlapping(ts1, ts3);

            //In case return error user allowed to see what is the return error 
            Console.WriteLine(result.ToString());

            //This is to check if return valued is return as expected for subordinate 3 
            Assert.IsFalse(result, "Same schedule diffrent employee id - Overlapping - Test failed");

            //same employee id with first schedule finish time in between second schedule
            //first schedule (08:30am - 12:00pm) and second schedule (09:00am - 05:00pm)
            result = _scheduleService.isOverlapping(ts4, ts1);

            //In case return error user allowed to see what is the return error 
            Console.WriteLine(result.ToString());

            //This is to check if return valued is return as expected for subordinate 3 
            Assert.IsTrue(result, "Same employee id with first schedule finish time in between second schedule - Overlapping - Test failed");

            //same employee id with second schedule finish time in between second schedule
            //first schedule (09:00am - 05:00pm) and second schedule (08:30am - 12:00pm)
            result = _scheduleService.isOverlapping(ts1, ts4);

            //In case return error user allowed to see what is the return error 
            Console.WriteLine(result.ToString());

            //This is to check if return valued is return as expected for subordinate 3 
            Assert.IsTrue(result, "Same employee id with second schedule finish time in between second schedule - Overlapping - Test failed");

            //same employee id with no overlapping 
            //employee 1 (08:30am - 12:00pm) and employee 1 (12:30pm - 03:00pm)
            result = _scheduleService.isOverlapping(ts4, ts5);

            //In case return error user allowed to see what is the return error 
            Console.WriteLine(result.ToString());

            //This is to check if return valued is return as expected for subordinate 3 
            Assert.IsFalse(result, "Same employee id with no overlapping - No overlapping - Test failed");

        }

    }
}
