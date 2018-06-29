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


        //Check the insert object role
        [Test]
        public void checkSetRoles()
        {
            List<Role> roleList =  _userService.setRoles(Path.GetFullPath(@"roles.json"));   
            
            Assert.IsNotEmpty(roleList, "Unable to insert roles.json to role object");           
        }

        //Check the insert object user
        [Test]
        public void checkSetUsers()
        {
            List<User> userList =  _userService.setUsers(Path.GetFullPath(@"users.json"));   
            
            Assert.IsNotEmpty(userList, "Unable to insert users.json to user object");           
            
        }

        [TestCase(3)]
        public void validateResult1(int value)
        {

            _userService.userList =  _userService.setUsers(Path.GetFullPath(@"users.json"));  
            _userService.roleList =  _userService.setRoles(Path.GetFullPath(@"roles.json")); 
            string result = _userService.getSubOrdinates(value);

            Console.WriteLine(result);

            Assert.AreEqual(result,
             @"[{""Id"":2,""Name"":""Emily Employee"",""Role"":4},{""Id"":5,""Name"":""Steve Trainer"",""Role"":5}]",
              "Failed to validate result " + value.ToString());
 
        }

        [TestCase(1)]
        public void validateResult2(int value)
        {

            _userService.userList =  _userService.setUsers(Path.GetFullPath(@"users.json"));  
            _userService.roleList =  _userService.setRoles(Path.GetFullPath(@"roles.json")); 
            string result = _userService.getSubOrdinates(value);

            Console.WriteLine(result);

            Assert.AreEqual(result,
             @"[{""Id"":2,""Name"":""Emily Employee"",""Role"":4},{""Id"":3,""Name"":""Sam Supervisor"",""Role"":3},{""Id"":4,""Name"":""Mary Manager"",""Role"":2},{""Id"":5,""Name"":""Steve Trainer"",""Role"":5}]",
              "Failed to validate result for subordinate " + value.ToString());
 
        }

    }
}
