using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace Advertisments
{
    class Program
    {
        public const Languages LANGUAGE = Languages.Ukrainian;

        static void Main(string[] args)
        {
            Person Oleg = new Person("Олег", "+380685864200", "blabla@mymail.ua");
            Person Dima = new Person("Дмитро", "0963246776", "moyaposhta@mymail.ua");
            Person Maria = new Person("Марiя", "0996785467", "nardisp@df.ua");
            Person Fedya = new Person("Федiр", "3769884567", "dfghjdf.fds");
            Advertisment ad1 = new Advertisment("Продаж птахiв", ServiceType.Комерція, Oleg, "231$");
            Advertisment ad2 = new Advertisment("Чернiвецький сайт знайомств", ServiceType.Знайомства, Dima);
            Advertisment ad3 = new Advertisment("Вiкна, дверi", ServiceType.Комерція, Maria, "344$");
            
            AdvertismentsList lst = new AdvertismentsList(ReadWriteMethods.WriteBinaryFile, ad1, ad2, ad3);
            Advertisment ad4 = new Advertisment("Пологовий будинок", ServiceType.Медицина, Fedya, "");
            lst.AddAdveertisment(ad4);
            
            lst.OrderListBy("Price");
            Console.ReadLine();
        }
    }
}
