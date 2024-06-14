using InsurTech.Core.Entities;

namespace InsurTech.APIs.DTOs.HealthInsurancePlanDTO
{
    public class AllHealthInsuranceDTO
    {
        public int Id { get; set; }
        public decimal YearlyCoverage { get; set; }
        public InsurancePlanLevel Level { get; set; }
        public string Category { get; set; }
        public decimal Quotation { get; set; }
        public string Company { get; set; }
        public string MedicalNetwork { get; set; }
        public decimal ClinicsCoverage { get; set; }
        public decimal HospitalizationAndSurgery { get; set; }
        public decimal OpticalCoverage { get; set; }
        public decimal DentalCoverage { get; set; }
    }
}
