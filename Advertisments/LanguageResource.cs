using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
namespace Advertisments
{
    public static class LanguageResource
    {
        private const string AD_VALIDATOR_SOURCE = "Validator_strings.xml";
        public static string[] GetValidatorStrings(Languages lang)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            
            List<string> resultStrings = new List<string>();
            XmlReader read = XmlReader.Create(AD_VALIDATOR_SOURCE, settings);
            while (read.Name != lang.ToString())
                read.Read();
            read.Read();
            while (read.Name != lang.ToString()) 
            {
                switch (read.Value)
                {
                    case "invalid_phone":
                        read.Read();
                        read.Read();
                        read.Read();
                        resultStrings.Add(read.Value);
                        break;
                    case "too_long_name":
                        read.Read();
                        read.Read();
                        read.Read();
                        resultStrings.Add(read.Value);
                        break;
                    case "wrong_email":
                        read.Read();
                        read.Read();
                        read.Read();
                        resultStrings.Add(read.Value);
                        break;
                    case "ok":
                        read.Read();
                        read.Read();
                        read.Read();
                        resultStrings.Add(read.Value);
                        break;
                    default: read.Read();
                        break;
                }
            }
            return resultStrings.ToArray();
        }
    }
}
