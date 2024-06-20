using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.CustomeValidation
{
    public class CheckBirthDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int MIN_AGE = 18;
            int MAX_AGE = 199;
            try
            {
                var birthDate = (string)value;

                var date = DateOnly.Parse(birthDate);

                var currentDate = DateOnly.FromDateTime(DateTime.Now);

                if (date > currentDate)
                {
                    return new ValidationResult("Birth date can not be in the future");
                }

                var age = currentDate.Year - date.Year;

                if (currentDate.Month < date.Month || (currentDate.Month == date.Month && currentDate.Day < date.Day))
                {
                    age--;
                }

                if (age < MIN_AGE)
                {
                    return new ValidationResult("You must be at least 18 years old to register");
                }

                if (age > MAX_AGE)
                {
                    return new ValidationResult("You can not register if you are older than 199 years old, please contact support to find a tombstone insurance");
                }

                return ValidationResult.Success;

            }
            catch
            {
                return new ValidationResult("Please enter a valid birth date");
            }
        }

    }
}
