using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace Schedule.Services
{

    public class ScheduleSerivce
    {
        //Check the overlapping timeshedule 
        public bool isOverlapping(TimeSchedule ts1, TimeSchedule ts2)
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
        public TimeSchedule getSchedule(string json)
        {
            try
            {
                TimeSchedule timeS = new TimeSchedule();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(timeS.GetType());
                timeS = ser.ReadObject(ms) as TimeSchedule;
                ms.Close();

                return timeS;
            }
            catch (System.Exception)
            {
                return null;
            }

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
