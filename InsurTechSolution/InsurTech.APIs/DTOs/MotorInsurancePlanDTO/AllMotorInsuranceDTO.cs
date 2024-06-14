using InsurTech.Core.Entities;

namespace InsurTech.APIs.DTOs.MotorInsurancePlanDTO
{
    public class AllMotorInsuranceDTO
    {

        public int Id { get; set; }
        public decimal YearlyCoverage { get; set; }
        public InsurancePlanLevel Level { get; set; }
        public string Category { get; set; }
        public decimal Quotation { get; set; }
        public string Company { get; set; }
        public decimal PersonalAccident { get; set; }
        public decimal Theft { get; set; }
        public decimal ThirdPartyLiability { get; set; }
        public decimal OwnDamage { get; set; }
        public decimal LegalExpenses { get; set; }
    }
}
