using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.API.Validators
{
    public class CustomerValidator : AbstractValidator<Models.Customer>
	{
		public CustomerValidator()
		{

            RuleFor(x => x.Id).NotNull();

			RuleFor(x => x.Surename).MinimumLength(3).Matches("[a-zA-Z]");

			RuleFor(x => x.DateOfBirth).Must(BeAValidDate).WithMessage("Date of birth is not in the correct format");

            RuleFor(x => x.Address.HouseNo.ToString()).Matches("^-[0-9]+$|^[0-9]+$");

            RuleFor(x => x.Address.Postcode).NotNull().Matches("^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9]?[A-Za-z]))))\\s?[0-9][A-Za-z]{2})$");

            RuleForEach(x => x.ContactInformation).SetValidator(new ContactInformationValidator());

            RuleFor(x => x.ContactInformation).Must(x => x.Count() >= 1).WithMessage("Must have 1 type of contact information, either EmailAddress or Telelphone");

        }

        private bool BeAValidDate(string date)
        {
            //ADD THIS CHECK AS DOB CAN BE EMPTY
            if (string.IsNullOrEmpty(date))
            {
                return true;
            }

			string[] formats = { "dd/MM/yyyy", "yyyy/MM/dd", "dd/MM/yyyy HH:mm", "yyyy/MM/dd HH:mm"};

			try
            {
                var dateTime = DateTime.ParseExact(date, formats, new CultureInfo("en-GB"), DateTimeStyles.None);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

	}
}
