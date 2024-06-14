using InsurTech.APIs.DTOs.HealthInsurancePlanDTO;
using InsurTech.APIs.DTOs.HomeInsurancePlanDTO;
using InsurTech.APIs.DTOs.MotorInsurancePlanDTO;
using InsurTech.Core.Entities;

namespace InsurTech.APIs.DTOs
{
    public class InsurancePlanByUserIdDTO
    {
        public List<HealthInsuranceDTO>? HealthInsurancePlans { get; set; } = new List<HealthInsuranceDTO>();
        public List<MotorInsuranceDTO>? MotorInsurancePlans { get; set; } = new List<MotorInsuranceDTO>();
        public List<HomeInsuranceDTO>? HomeInsurancePlans { get; set; } = new List<HomeInsuranceDTO>();
    }
}
