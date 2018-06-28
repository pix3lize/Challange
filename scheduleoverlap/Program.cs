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
            // sample data for same employee id 1 schedule for 26 June 2018 09:00 - 17:00
            string json1 = @"{
            ""Id"": 1,
            ""Employee"": 1,
            ""Department"": 1,
            ""StartTime"": 1530054000,
            ""EndTime"": 1530082800
            }";

            // sample data for same employee id 1 schedule for 26 June 2018 09:00 - 17:00
            string json2 = @"{
            ""Id"": 1,
            ""Employee"": 1,
            ""Department"": 1,
            ""StartTime"": 1530054000,
            ""EndTime"": 1530082800

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

            Console.WriteLine(isOverlapping(ts1, ts2));
            Console.WriteLine(isOverlapping(ts1, ts3));
        }

        public static bool isOverlapping(TimeSchedule ts1, TimeSchedule ts2)
        {
            if (ts1.Id == ts2.Id)
            {
                if ((ts2.StartTime <= ts1.StartTime) && (ts2.StartTime <= ts1.EndTime))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

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
