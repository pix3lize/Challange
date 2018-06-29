using NUnit.Framework;
using UserH.Services;
using System.Collections.Generic;
using System.IO; 
using System;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace UserH.UnitTests.Services
{
    [TestFixture]
    public class UserService_Test
    {
        private readonly UserService _userService;

        public UserService_Test()
        {
         
            _userService = new UserService();
            var roleTest = new Role(); 
        }


        //Check the insert object role with json file
        [Test]
        public void checkSetRoles()
        {
            List<Role> roleList =  _userService.setRoles(Path.GetFullPath(@"roles.json"));   

            //This is to check if the object is not empty or any error return from the function will make object into null
            Assert.IsNotEmpty(roleList, "Unable to insert roles.json to role object");           
        }

        //Check the insert object user with json file 
        [Test]
        public void checkSetUsers()
        {
            List<User> userList =  _userService.setUsers(Path.GetFullPath(@"users.json"));   

            //This is to check if the object is not empty or any error return from the function will make object into null
            Assert.IsNotEmpty(userList, "Unable to insert users.json to user object");           
            
        }

        //Check the value for the getsubordinate 3 
        [TestCase(3)]
        public void validateResult1(int value)
        {
            //Initialize the user list object and role list object 
            _userService.userList =  _userService.setUsers(Path.GetFullPath(@"users.json"));  
            _userService.roleList =  _userService.setRoles(Path.GetFullPath(@"roles.json")); 

            //Run run the subordinate method to return value
            string result = _userService.getSubOrdinates(value);

            //In case return error user allowed to see what is the return error 
            Console.WriteLine(result);

            //This is to check if return valued is return as expected for subordinate 3 
            Assert.AreEqual(result,
             @"[{""Id"":2,""Name"":""Emily Employee"",""Role"":4},{""Id"":5,""Name"":""Steve Trainer"",""Role"":5}]",
              "Failed to validate result " + value.ToString());
 
        }

        //Check the value for the getsubordinate 1
        [TestCase(1)]
        public void validateResult2(int value)
        {
            //Initialize the user list object and role list object 
            _userService.userList =  _userService.setUsers(Path.GetFullPath(@"users.json"));  
            _userService.roleList =  _userService.setRoles(Path.GetFullPath(@"roles.json")); 

            //Run run the subordinate method to return value
            string result = _userService.getSubOrdinates(value);

            //In case return error user allowed to see what is the return error 
            Console.WriteLine(result);

            //This is to check if return valued is return as expected for subordinate 1
            Assert.AreEqual(result,
             @"[{""Id"":2,""Name"":""Emily Employee"",""Role"":4},{""Id"":3,""Name"":""Sam Supervisor"",""Role"":3},{""Id"":4,""Name"":""Mary Manager"",""Role"":2},{""Id"":5,""Name"":""Steve Trainer"",""Role"":5}]",
              "Failed to validate result for subordinate " + value.ToString());
 
        }

    }
}
