using InsurTech.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.DTOs.Company
{
    public class CompanyByIdOutputDto
    {
       
        public string Id { get; set; }
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string TaxNumber { get; set; }

        public string Location { get; set; }

        public string PhoneNumber { get; set; }

        public int InsurancePlansCount { get; set; }

        public ICollection<string> Roles { get; set; }


    }
}
