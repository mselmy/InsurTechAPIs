namespace InsurTech.APIs.DTOs.RequestDTO
{
    public class ApplyForInsurancePlanInput
    {

        public string CustomerId { get; set; }
        public int InsurancePlanId { get; set; }
        public List<RequestAnswersDto> Answers { get; set; }
    }
}
