using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisments
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Person(string Name,string PhoneNumber,string Email)
        {
            this.Name = Name;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
        }
    }
}
