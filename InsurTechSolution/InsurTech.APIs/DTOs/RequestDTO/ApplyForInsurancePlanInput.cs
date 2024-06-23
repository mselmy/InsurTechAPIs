using System.ComponentModel.DataAnnotations;

namespace InsurTech.APIs.DTOs.RequestDTO
{
    public class ApplyForInsurancePlanInput
    {

        //public string CustomerId { get; set; }
        [Required]
        public int InsurancePlanId { get; set; }
        [Required]
        public List<RequestAnswersDto> Answers { get; set; }
    }
}
