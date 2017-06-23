using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace Advertisments
{
    
    class AdvertismentValidator : Validator
    {
        private static string[] VALIDATOR_STRINGS = LanguageResource.GetValidatorStrings(Program.LANGUAGE);

        const int WRONG_TEL_NUMBER = 0;
        const int BIG_AD_NAME = 1;
        const int WRONG_EMAIL = 2;
        const int IS_VALID_INPUT = 3;
        private string resultMessage;
        private Advertisment AdForCheck;
        public string Message
        {
            get
            {
                if (resultMessage == string.Empty)
                    return "Перевiрка не вiдбувалась";
                else
                    return this.resultMessage;
            }
            
        }
        public AdvertismentValidator(Advertisment adForCheck)
        {
            this.AdForCheck = adForCheck;
            this.resultMessage = string.Empty;
        }

        public bool IsValid()
        {
            Regex regExpr1 = new Regex(@"^[+]380\d{9}");
            Regex regExpr2 = new Regex("^0");
            bool isRight = true;
            StringBuilder buildResultStr = new StringBuilder();
            if (AdForCheck.NameOfAd.Length > 100)
            {
                buildResultStr.AppendLine(VALIDATOR_STRINGS[BIG_AD_NAME]);
                isRight = false;               
            }
            if (!regExpr1.IsMatch(AdForCheck.Who.PhoneNumber))  
            {
                if (!regExpr2.IsMatch(AdForCheck.Who.PhoneNumber) || regExpr2.IsMatch(AdForCheck.Who.PhoneNumber) && AdForCheck.Who.PhoneNumber.Length != 10)
                {
                    buildResultStr.AppendLine(VALIDATOR_STRINGS[WRONG_TEL_NUMBER] + ":" + AdForCheck.Who.PhoneNumber);
                    isRight = false;
                }
            }
            if (!AdForCheck.Who.Email.Contains('@'))
            {
                buildResultStr.AppendLine(VALIDATOR_STRINGS[WRONG_EMAIL]);
                isRight = false;
            }
            if (isRight)
                buildResultStr.AppendLine(VALIDATOR_STRINGS[IS_VALID_INPUT]);
            this.resultMessage = buildResultStr.ToString();
            return isRight;
        }
    }
}
