using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace scheduleoverlap
{
    class Program
    {
        static void Main(string[] args)
        {
            // sample data for same employee id 1 schedule for 27 June 2018 09:00am - 17:00pm
            string json1 = @"{
            ""Id"": 1,
            ""Employee"": 1,
            ""Department"": 1,
            ""StartTime"": 1530054000,
            ""EndTime"": 1530082800
            }";

            // sample data for same employee id 1 schedule for 27 June 2018 09:00am - 17:00pm
            string json2 = @"{
            ""Id"": 1,
            ""Employee"": 1,
            ""Department"": 1,
            ""StartTime"": 1530054000,
            ""EndTime"": 1530082800

            }";

            // sample data for same employee id 1 schedule for 27 June 2018 08:30am - 12:00pm
            string json4 = @"{
            ""Id"": 1,
            ""Employee"": 1,
            ""Department"": 1,
            ""StartTime"": 1530052200,
            ""EndTime"": 1530064800
            }";

            // sample data for same employee id 1 schedule for 27 June 2018 12:30am - 15:00pm
            string json5 = @"{
            ""Id"": 1,
            ""Employee"": 1,
            ""Department"": 1,
            ""StartTime"": 1530066600,
            ""EndTime"": 1530075600
            }";

            // sample data for same employee id 2 schedule for 26 June 2018 09:00 - 17:00
            string json3 = @"{
            ""Id"": 2,
            ""Employee"": 1,
            ""Department"": 1,
            ""StartTime"": 1530054000,
            ""EndTime"": 1530082800
            }";

            TimeSchedule ts1 = getSchedule(json1);
            TimeSchedule ts2 = getSchedule(json2);
            TimeSchedule ts3 = getSchedule(json3);
            TimeSchedule ts4 = getSchedule(json4);
            TimeSchedule ts5 = getSchedule(json5);

            //same timing with same employee id
            Console.WriteLine("Same timing with same employee id - Overlapping");
            Console.WriteLine(isOverlapping(ts1, ts2));
            Console.WriteLine("");

            //same timing but different employee id 
            Console.WriteLine("Same schedule diffrent employee id - Overlapping");
            Console.WriteLine(isOverlapping(ts1, ts3));
            Console.WriteLine("");

            //same employee id with first schedule finish time in between second schedule
            //first schedule (08:30am - 12:00pm) and second schedule (09:00am - 05:00pm)
            Console.WriteLine("Same employee id with first schedule finish time in between second schedule - Overlapping");
            Console.WriteLine(isOverlapping(ts4, ts1));
            Console.WriteLine("");

            //same employee id with second schedule finish time in between second schedule
            //first schedule (09:00am - 05:00pm) and second schedule (08:30am - 12:00pm)
            Console.WriteLine("Same employee id with second schedule finish time in between second schedule - Overlapping");
            Console.WriteLine(isOverlapping(ts1, ts4));
            Console.WriteLine("");

            //same employee id with no overlapping 
            //employee 1 (08:30am - 12:00pm) and employee 1 (12:30pm - 03:00pm)
            Console.WriteLine("Same employee id with no overlapping - No overlapping");
            Console.WriteLine(isOverlapping(ts4, ts5));
            Console.WriteLine("");

        }

        //Check the overlapping timeshedule 
        public static bool isOverlapping(TimeSchedule ts1, TimeSchedule ts2)
        {
            //Check if both schedule using same employee id
            if (ts1.Id == ts2.Id)
            {
                //To check if second schedule start time higher than first schedule start time 
                // and second schedule start time lower than first schedule finish time
                if ((ts2.StartTime >= ts1.StartTime) && (ts2.StartTime <= ts1.EndTime))
                {
                    return true;
                }
                //To check if first schedule start time higher than second schedule start time 
                // and first schedule start time lower than second schedule finish time
                else if ((ts1.StartTime >= ts2.StartTime) && (ts1.StartTime <= ts2.EndTime))
                {
                    return true; 
                }
                //No overlapping return false 
                else
                {
                    return false;
                }
            }
            //Diffrent employee id return false
            else
            {
                return false;
            }
        }

        //Initialise time schedule object from json file
        public static TimeSchedule getSchedule(string json)
        {
            TimeSchedule timeS = new TimeSchedule();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(timeS.GetType());
            timeS = ser.ReadObject(ms) as TimeSchedule;
            ms.Close();

            return timeS;
        }
    }
    [DataContract]
    public class TimeSchedule
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Employee { get; set; }
        [DataMember]
        public int Department { get; set; }
        [DataMember]
        public int StartTime { get; set; }
        [DataMember]
        public int EndTime { get; set; }
    }
}
