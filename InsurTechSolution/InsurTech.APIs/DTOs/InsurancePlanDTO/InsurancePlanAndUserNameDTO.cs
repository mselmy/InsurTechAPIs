namespace InsurTech.APIs.DTOs.InsurancePlanDTO
{
    public class InsurancePlanAndUserNameDTO
    {
        public InsurancePlanForCompanyDTO? InsurancePlan { get; set; }
        public List<string>? Usernames { get; set; }
    }
}
