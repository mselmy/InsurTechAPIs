using InsurTech.Core.Entities;

namespace InsurTech.APIs.DTOs.InsurancePlanDTO
{
    public class InsurancePlanForCompanyDTO
    {
        public string CategoryName { get; set; }
        public InsurancePlanLevel Level { get; set; }
        public decimal Quotation { get; set; }
    }
}
