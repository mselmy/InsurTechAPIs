using InsurTech.Core.Entities;

namespace InsurTech.APIs.DTOs.RequestDTO
{
    public class RequestUpdatedDto
    {
        public string Id { get; set; }
        public RequestStatus Status { get; set; }

    }
}
