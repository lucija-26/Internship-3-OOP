using Internship_3_OOP.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP.Telefonski_imenik
{
    public class PhoneCall
    {
        public DateTime TimeOfCall;
        public CallStatus CallStatus {  get; set; }

        public int Duration { get; }

        public PhoneCall(DateTime timeOfCall, CallStatus callStatus, int duration)
        {
            TimeOfCall = timeOfCall;
            CallStatus = callStatus;
            Duration = duration;
        }
    }
}
