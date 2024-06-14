using InsurTech.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.CustomeValidation
{
    public class CheckLevelAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            InsurancePlanLevel InsurancePlanLevel = (InsurancePlanLevel)value;
            if (Enum.IsDefined(typeof(InsurancePlanLevel), InsurancePlanLevel))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("please enter a valid Level");
        }
    }
}
