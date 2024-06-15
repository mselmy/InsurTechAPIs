using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.CustomeValidation
{
    public class CheckBirthDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var birthDate = (string)value;

                var date = DateOnly.Parse(birthDate);


                var currentYear = DateTime.Now.Year;


                if (date.Year > 1900 && date.Year < currentYear)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Please enter birth date between 1900 and the current year");


            }
            catch
            {
                return new ValidationResult("Please enter a valid birth date");
            }
        }

    }
}
