using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisments
{
    public delegate void WriteFileMethod(AdvertismentsList AdsList,string FileName);

    public class AdvertismentsList:IEnumerable<Advertisment>
    {
        private List<Advertisment> Advertisments;
        private WriteFileMethod MethodForWriting;
        public AdvertismentsList(WriteFileMethod MethodForWriting, params Advertisment[] MyAdvertisments)
        {
            this.MethodForWriting = MethodForWriting;
            Advertisments = new List<Advertisment>();
            ValidateAndAdd(MyAdvertisments);
        }

        private void ValidateAndAdd(params Advertisment[] inputAds)
        {
            foreach (Advertisment ad in inputAds)
            {
                AdvertismentValidator validator = new AdvertismentValidator(ad);
                if (validator.IsValid())
                    Advertisments.Add(ad);
                else
                    Console.WriteLine(validator.Message);
            }
        }

        public void AddAdveertisment(Advertisment ad)
        {
            ValidateAndAdd(ad);
        }

        public void WriteToFile(string FIleName)
        {
            MethodForWriting(this, FIleName);
        }
        public void SetWriteFileMethod(WriteFileMethod MethodForWriting)
        {
            this.MethodForWriting = MethodForWriting;
        }

        public void PrintInConsole()
        {
            foreach (Advertisment ad in this.Advertisments)
            {
                Console.WriteLine(ad.ToString());
            }
        }

        public IEnumerator<Advertisment> GetEnumerator()
        {
            return this.Advertisments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Advertisments.GetEnumerator();
        }

        public void OrderListBy(string field)
        {
            switch(field)
            {
                case "NameOfAd":
                    var resultOrder = from tempAd in this.Advertisments
                                  orderby tempAd.NameOfAd
                                  select tempAd;
                    this.Advertisments = resultOrder.ToList();
                    break;
                case "Price":
                    Dictionary<Advertisment, int> prices = new Dictionary<Advertisment, int>();
                    foreach (Advertisment ad in this.Advertisments)
                    {
                        if (ad.Price == "")
                            prices.Add(ad, 0);
                        else
                            prices.Add(ad, int.Parse(ad.Price.Substring(0, ad.Price.Length - 1)));
                    }
                    var orderedAds = from tmp in prices
                                     orderby tmp.Value ascending
                                     select tmp;
                    this.Advertisments = new List<Advertisment>();
                    foreach (var ad in orderedAds)
                        Advertisments.Add(ad.Key);
                    break;
                case "ServiceType":
                    break;
                case "PersonName":
                    break;
                case "PhoneNumber":
                    break;
            }
        }

    }
}
