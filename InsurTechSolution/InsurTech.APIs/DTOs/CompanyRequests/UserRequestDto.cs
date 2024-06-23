using InsurTech.Core.Entities;

namespace InsurTech.APIs.DTOs.CompanyRequests
{
    public class UserRequestDto
    {
        
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string InsurancePlanName { get; set; }
        public InsurancePlanLevel Level { get; set; }
        public string CompanyName { get; set; }
        public ICollection<RequestQuestionDto> RequestQuestions { get; set; }
        public RequestStatus Status { get; set; }
    }
}
