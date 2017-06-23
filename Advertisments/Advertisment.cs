using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisments
{
    [Serializable]
    public class Advertisment
    {
        public string NameOfAd { get;  set; }
        public string Price { get;  set; }
        public ServiceType ServiceType { get;  set; }
        public Person Who { get;  set; }

        public Advertisment(string NameOfAd, ServiceType ServiceType,
            Person Who, string Price = "")
        {
            this.NameOfAd = NameOfAd;
            this.Price = Price;
            this.ServiceType = ServiceType;
            this.Who = Who;
        }
        public Advertisment() { }

        public override string ToString()
        {
            switch(Price)
            {
                case "":
                    return string.Format("\t{0}\nТип оголошення:{1}\nЗвертатися до:{2}\nНомер телефону:{3}", NameOfAd, ServiceType, Who.Name, Who.PhoneNumber);
                default:
                    return string.Format("\t{0}\nТип оголошення:{1}\nЦiна послуги:{2}\nЗвертатися до:{3}\nНомер телефону:{4}", NameOfAd, ServiceType, Price, Who.Name, Who.PhoneNumber);                 
            }
            
        }
    }
    public enum ServiceType
    {
        Медицина,
        Навчання,
        Комерція,
        Знайомства
    }
}
