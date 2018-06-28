using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace userhierarchies
{

    class Program
    {
        public static List<Role> roleList = new List<Role>();
        public static List<User> userList = new List<User>();
        // Result for expected lookUpRoleId
        public static List<int> lookUpRoleId = new List<int>();

        static void Main(string[] args)
        {
            // Populate object from text file, this input could be http request 
            roleList = setRoles("roles.json");
            userList = setUsers("users.json");

            // Calling get sub ordinate function to return json output
            Console.WriteLine("");
            Console.WriteLine("Testing subordinate 3");
            Console.WriteLine(getSubOrdinates(3) );
            if(getSubOrdinates(3) == @"[{""Id"":2,""Name"":""Emily Employee"",""Role"":4},{""Id"":5,""Name"":""Steve Trainer"",""Role"":5}]")
            {
                Console.WriteLine("Testing SUCCESS"+ "\r\n");
            }
            else 
            {
                Console.WriteLine("Testing FAILED"+ "\r\n");
            }
            
            Console.WriteLine("Testing subordinate 1 : ");
            Console.WriteLine(getSubOrdinates(1));

            if(getSubOrdinates(1) == @"[{""Id"":2,""Name"":""Emily Employee"",""Role"":4},{""Id"":3,""Name"":""Sam Supervisor"",""Role"":3},{""Id"":4,""Name"":""Mary Manager"",""Role"":2},{""Id"":5,""Name"":""Steve Trainer"",""Role"":5}]")
            {
                Console.WriteLine("Testing SUCCESS"+ "\r\n");
            }
            else 
            {
                Console.WriteLine("Testing FAILED"+ "\r\n");
            }
            //Console.WriteLine("Hello World!");
        }

        // Get list of role object from json file 
        public static List<Role> setRoles(string path)
        {
            try
            {
                StreamReader sr = new StreamReader(path);
                string json = sr.ReadToEnd();
                sr.Close();

                List<Role> lRole = new List<Role>();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(lRole.GetType());
                lRole = ser.ReadObject(ms) as List<Role>;
                ms.Close();

                return lRole;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex.Message);
                return null;
            }
        }

        // Get list of user object from json file 
        public static List<User> setUsers(string path)
        {
            try
            {
                StreamReader sr = new StreamReader(path);
                string json = sr.ReadToEnd();
                sr.Close();

                List<User> lUser = new List<User>();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(lUser.GetType());
                lUser = ser.ReadObject(ms) as List<User>;
                ms.Close();

                return lUser;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex.Message);
                return null;
            }
        }

        // Function to get subordinates from given user id 
        public static string getSubOrdinates(int id)
        {
            // Clear global variable everytime this function get called
            lookUpRoleId = new List<int>();

            // First itereation to search data from userList and to look for role id 
            foreach (var item in userList)
            {
                // Look for the user id that match with search criteria 
                if (item.Id == id)
                {
                    // Recursive loop to look for children on search result 		
                    hasChildren(item.Role);
                }
            }

            #region Collapse

            //Array of an object for our search user result 
            List<User> resultUser = new List<User>();

            //Third iteration to search data from userList 
            foreach (var item in userList)
            {
                //Fourth iteration to get userlist that have same role id as lookupRoleId 
                foreach (var item2 in lookUpRoleId)
                {
                    if (item.Role == item2)
                    {
                        resultUser.Add(item);
                    }
                }
            }

            // Serialize the object into JSON text format 
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(List<User>));
            DataContractJsonSerializerSettings s = new DataContractJsonSerializerSettings();
            ds.WriteObject(stream, resultUser);
            string jsonString = Encoding.UTF8.GetString(stream.ToArray());
            stream.Close();
            return jsonString;            

            #endregion
        }

        // Recursive iteration to look into possible children of the object 
        public static void hasChildren(int id)
        {
            foreach (var item in roleList)
            {
                if (item.Parent == id)
                {
                    //(item.Id + " " + item.Name + " " + item.Parent).Dump("User Role");
                    // if the function found children, the role would be save as an array of int 
                    //in which later be used for parsing the user 
                    lookUpRoleId.Add(item.Id);
                    hasChildren(item.Id);
                }
            }
        }

        // Function to write an error code 
        public static void writeErrorLog(string message)
        {
            StreamWriter sw = new StreamWriter("error.log");
            sw.WriteLine(DateTime.Now.ToString() + " - " + message);
            sw.Close();
        }


    }

    [DataContract]
    public class Role
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Parent { get; set; }
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Role { get; set; }
    }

}
