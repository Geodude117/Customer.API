using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Customer.API.Validators
{
    public class ContactInformationValidator : AbstractValidator<Models.ContactInformation>
    {
        private string currentType;

        public ContactInformationValidator()
        {
            RuleFor(x => x.Type).NotNull().Must(IsValidType);
            RuleFor(x => x.Value).NotNull().Must(ContactInfoValidation);
        }

        private bool ContactInfoValidation(string contactInfoValue)
        {
            if (currentType == Models.ContactInfomationType.EmailAddress.ToString())
            {
                return this.IsValidEmailAddress(contactInfoValue);
            }
            else if (currentType == Models.ContactInfomationType.Telephone.ToString())
            {
                return this.IsValidPhone(contactInfoValue);
            }
            else
            {
                return false;
            }
        }
        public bool IsValidEmailAddress(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public bool IsValidPhone(string Phone)
        {
            try
            {
                if (string.IsNullOrEmpty(Phone))
                    return false;
                var r = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
                return r.IsMatch(Phone);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsValidType(string type)
        {
            try
            {
                if (Enum.IsDefined(typeof(Models.ContactInfomationType), type))
                {
                    currentType = type;
                    return true;
                }else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
