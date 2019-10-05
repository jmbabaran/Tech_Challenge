using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientAssistance.Models
{
    public class WaitingList
    {
        public int patientCount { get; set; }
        public int levelOfPain { get; set; }
        public int averageProcessTime { get; set; }
    }
}