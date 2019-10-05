using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientAssistance.Models
{
    public class Hospital
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<WaitingList> waitingList { get; set; }
        public int totalWaitingTime { get; set; }
        public string totalWaitingTimeInHrs { get; set; }


    }
}