using Internship_3_OOP.enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP.Telefonski_imenik
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public Preference Preference { get; set; }
        public List<PhoneCall> PhoneCalls { get; }
        public Contact(string firstName, string lastName, string phoneNumber, Preference preference)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Preference = preference;
            PhoneCalls = new List<PhoneCall>();
        }
        public void AddCall(PhoneCall phoneCall)
        {
            PhoneCalls.Add(phoneCall);
        }
    }
}
