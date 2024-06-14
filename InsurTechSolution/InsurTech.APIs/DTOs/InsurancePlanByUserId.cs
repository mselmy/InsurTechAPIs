using InsurTech.Core.Entities;

namespace InsurTech.APIs.DTOs
{
    public class InsurancePlanByUserId
    {
        public List<HealthInsurancePlan>? HealthInsurancePlans { get; set;} = new List<HealthInsurancePlan>();
        public List<MotorInsurancePlan>? MotorInsurancePlans { get; set; } = new List<MotorInsurancePlan>();
        public List<HomeInsurancePlan>? HomeInsurancePlans { get; set; } = new List<HomeInsurancePlan>();

    }
}
