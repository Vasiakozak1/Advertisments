using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;
namespace Advertisments
{
    public static class ReadWriteMethods
    {
        public static void WriteTxtFile(AdvertismentsList AdsList, string FileName)
        {
            StreamWriter writeFile = new StreamWriter(new FileStream(FileName + ".txt", FileMode.Create));
            using (writeFile)
            {
                foreach (Advertisment ad in AdsList)
                {
                    writeFile.WriteLine(ad.ToString());
                }
            }
        }
        public static void WriteXmlFile(AdvertismentsList AdsList, string FileName)
        {
            using (FileStream fileStr = new FileStream(FileName + ".xml", FileMode.Create))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.NewLineOnAttributes = true;
                settings.Indent = true;
                XmlWriter writeXml = XmlWriter.Create(fileStr, settings);
                
                
                writeXml.WriteStartDocument();
                writeXml.WriteStartElement(nameof(AdvertismentsList));

                foreach (Advertisment ad in AdsList)
                {
                    writeXml.WriteStartElement(nameof(Advertisment));
                    writeXml.WriteElementString(nameof(ad.NameOfAd), ad.NameOfAd);
                    writeXml.WriteElementString(nameof(ad.ServiceType), ad.ServiceType.ToString());
                    if (ad.Price != "")
                        writeXml.WriteElementString(nameof(ad.Price), ad.Price);

                        writeXml.WriteStartElement(nameof(Person));
                        writeXml.WriteElementString(nameof(ad.Who.Name), ad.Who.Name);
                        writeXml.WriteElementString(nameof(ad.Who.Email), ad.Who.Email);
                        writeXml.WriteElementString(nameof(ad.Who.PhoneNumber), ad.Who.PhoneNumber);
                        writeXml.WriteEndElement();
                    writeXml.WriteEndElement();
                }
                
                writeXml.WriteEndElement();
                writeXml.WriteEndDocument();
                writeXml.Close();
            }
            
        }
        public static void WriteBinaryFile(AdvertismentsList AdsList, string FileName)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            using (FileStream fs = new FileStream(FileName, FileMode.Create))
            {
                foreach (Advertisment ad in AdsList)
                {
                    binForm.Serialize(fs, ad);
                }
            }
        }

        public static void WriteJSONFile(AdvertismentsList AdsList, string FileName)
        {
            using (StreamWriter writeStr = new StreamWriter(new FileStream(FileName + ".json", FileMode.Create))) 
            {
                writeStr.Write('[');
                var lst = AdsList.ToList();
                foreach (Advertisment ad in AdsList)
                {
                    writeStr.WriteLine('{');
                    writeStr.WriteLine("\t\"{0}\": \"{1}\",", nameof(ad.NameOfAd), ad.NameOfAd);
                    if (ad.Price != "")
                        writeStr.WriteLine("\t\"{0}\": \"{1}\",", nameof(ad.Price), ad.Price);
                    writeStr.WriteLine("\t\"{0}\": \"{1}\",", nameof(ad.ServiceType), ad.ServiceType);
                    writeStr.Write("\t\"{0}\": ", nameof(Person));
                    writeStr.WriteLine('{');
                    writeStr.WriteLine("\t\t\"{0}\": \"{1}\",", nameof(ad.Who.Name), ad.Who.Name);
                    writeStr.WriteLine("\t\t\"{0}\": \"{1}\",", nameof(ad.Who.Email), ad.Who.Email);
                    writeStr.WriteLine("\t\t\"{0}\": \"{1}\"", nameof(ad.Who.PhoneNumber), ad.Who.PhoneNumber);
                    writeStr.WriteLine("\t\t}");
                    writeStr.Write('}');
                    if (!(lst.IndexOf(ad) == lst.Count - 1))
                        writeStr.WriteLine(',');

                }
                writeStr.Write(']');
            }
        }

        public static AdvertismentsList ReadJSONfile(string FileName)
        {
            List<Advertisment> ads = new List<Advertisment>();
            using (StreamReader readStr = new StreamReader(new FileStream(FileName, FileMode.Open)))
            {
                string nameOfAd = "";
                string phoneNumber = "";
                ServiceType servType = default(ServiceType);
                string Price = "";
                string personName = "";
                string eMail = "";

                while (!readStr.EndOfStream)
                {
                    Price = "";
                    readStr.ReadLine();
                    bool continueRead = true;
                    do
                    {
                        
                        string currentLine = readStr.ReadLine();
                        if (currentLine == null)
                            return new AdvertismentsList(WriteBinaryFile, ads.ToArray());
                        var lines = currentLine.Split(new char[] { '\"' }, StringSplitOptions.RemoveEmptyEntries).Where(n => char.IsDigit(n[0]) || char.IsLetter(n[0]) || n[0] == '+');
                        
                        string field = "";
                        string value = "";
                        foreach (string s in lines)
                        {
                            if (field == "")
                                field = s;
                            else
                                value = s;
                        }

                        switch (field)
                        {
                            case "NameOfAd":
                                nameOfAd = value;
                                break;
                            case "PhoneNumber":
                                phoneNumber = value;
                                break;
                            case "Price":
                                Price = value;
                                break;
                            case "ServiceType":

                                switch(value)
                                {
                                    case "Медицина":
                                        servType = ServiceType.Медицина;
                                        break;
                                    case "Навчання":
                                        servType = ServiceType.Навчання;
                                        break;
                                    case "Комерція":
                                        servType = ServiceType.Комерція;
                                        break;
                                    case "Знайомства":
                                        servType = ServiceType.Знайомства;
                                        break;
                                }
                                break;
                            case "Name":
                                personName = value;
                                break;
                            case "Email":
                                eMail = value;
                                break;
                            case "Person":                            
                                break;
                        }
                        if (field == "PhoneNumber")
                            continueRead = false;
                    } while (continueRead);
                    readStr.ReadLine();
                    ads.Add(new Advertisment(nameOfAd, servType, new Person(personName, phoneNumber, eMail), Price));
                }
            }
            return new AdvertismentsList(WriteBinaryFile, ads.ToArray());
        }

        public static AdvertismentsList ReadBinaryFile(string FileName)
        {
            List<Advertisment> ads = new List<Advertisment>();
            try
            {
                BinaryFormatter binForm = new BinaryFormatter();
                using (FileStream fileStr = new FileStream(FileName, FileMode.Open))
                {

                    while (fileStr.Position < fileStr.Length)
                    {
                        ads.Add(binForm.Deserialize(fileStr) as Advertisment);
                    }
                }
            }
            catch (IOException exc)
            {
                Console.WriteLine("Exception while read binary file:" + exc.Message);
            }
            return new AdvertismentsList(WriteBinaryFile, ads.ToArray());
        }
        public static AdvertismentsList ReadXmlFile(string FileName)
        {
            List<Advertisment> ads = new List<Advertisment>();
            using (XmlReader xmlRead = XmlReader.Create(FileName))
            {
                string nameOfAd = "";
                string phoneNumber = "";
                ServiceType servType = default(ServiceType);
                string price = "";
                string Who = "";
                string eMail = "";
                Person person;
                while (xmlRead.Read())
                {
                    if (xmlRead.IsStartElement())
                    {
                        switch(xmlRead.Name)
                        {
                            case "NameOfAd":
                                xmlRead.Read();
                                nameOfAd = xmlRead.Value;
                                break;
                            
                            case "ServiceType":
                                xmlRead.Read();
                                switch(xmlRead.Value)
                                {
                                    case "Медицина":
                                        servType = ServiceType.Медицина;
                                        break;
                                    case "Навчання":
                                        servType = ServiceType.Навчання;
                                        break;
                                    case "Комерція":
                                        servType = ServiceType.Комерція;
                                        break;
                                    case "Знайомства":
                                        servType = ServiceType.Знайомства;
                                        break;
                                }
                                break;
                            case "Price":
                                xmlRead.Read();
                                price = xmlRead.Value;
                                break;
                            case "Email":
                                xmlRead.Read();
                                eMail = xmlRead.Value;
                                break;
                            case "Name":
                                xmlRead.Read();
                                Who = xmlRead.Value;                          
                                break;
                            case "PhoneNumber":
                                xmlRead.Read();
                                phoneNumber = xmlRead.Value;
                                person = new Person(Who, phoneNumber, eMail);
                                ads.Add(new Advertisment(nameOfAd, servType, person, price));
                                nameOfAd = "";
                                phoneNumber = "";
                                servType = default(ServiceType);
                                price = "";
                                Who = "";
                                eMail = "";
                                break;
                        }
                        
                    }
                }
               
            }
            return new AdvertismentsList(WriteBinaryFile, ads.ToArray());
        }
        
    }
}
