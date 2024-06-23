using InsurTech.Core.Entities.Identity;

namespace InsurTech.APIs.DTOs.NotificationDTO
{
    public class GetNotificationDTO
    {
        public string Body { get; set; }
        public string UserId { get; set; }
        public bool IsRead { get; set; }
    }
}
