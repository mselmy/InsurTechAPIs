using InsurTech.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.CustomeValidation
{
    public class CheckCompanyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var identityservices = (UserManager<IdentityRole>)validationContext.GetService(typeof(UserManager<IdentityRole>));
            var validationtask = IsValidAsyn(value, identityservices);
            validationtask.Wait();
            return validationtask.Result;

        }
        private async Task<ValidationResult> IsValidAsyn(object value, UserManager<IdentityRole> userManager)
        {
            string companyId = (string)value;
            int.TryParse(companyId, out var id);
            var companyRolesresult = await userManager.FindByIdAsync($"{id}");
            if (companyRolesresult == null)
            {
                return new ValidationResult("This Company is not exists");
            }
            if (!companyRolesresult.Name.Contains("Company"))
            {
                return new ValidationResult("This Company is not exists");
            }
            return ValidationResult.Success;

        }
    }
}