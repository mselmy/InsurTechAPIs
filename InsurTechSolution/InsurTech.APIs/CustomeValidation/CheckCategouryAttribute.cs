using InsurTech.Core.Entities;
using InsurTech.Core.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public class CheckCategouryAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var categoriesRepository = (IGenericRepository<Category>)validationContext.GetService(typeof(IGenericRepository<Category>));
        var validationTask = IsValidAsync(value, categoriesRepository);
        validationTask.Wait();
        return validationTask.Result;
    }

    private async Task<ValidationResult> IsValidAsync(object value, IGenericRepository<Category> categoriesRepository)
    {
        int CategouryId = (int)value;
        var registeredCategories = await categoriesRepository.GetAllAsync();
        if (registeredCategories.Any(c => c.Id == CategouryId))
        {
            return ValidationResult.Success;
        }
        return new ValidationResult("Please enter a valid categoury");
    }
}
