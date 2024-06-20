using InsurTech.Core;
using InsurTech.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetNotifications")]
        public async Task<IActionResult> GetNotifications()
        {
            try
            {
                var notifications = await _unitOfWork.Repository<Notification>().GetAllAsync();
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching notifications.", details = ex.Message });
            }
        }

        [HttpGet("NumberOfUnreadNotifications")]
        public async Task<IActionResult> NumberOfUnreadNotifications(string id)
        {
            try
            {
                var notifications = await _unitOfWork.Repository<Notification>().GetAllAsync();
                var unreadCount = notifications.Count(a => !a.IsRead && a.UserId == id);
                return Ok(unreadCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching unread notification count.", details = ex.Message });
            }
        }

        [HttpGet("HasUnreadNotifications")]
        public async Task<IActionResult> HasUnreadNotifications(string id)
        {
            try
            {
                var notifications = await _unitOfWork.Repository<Notification>().GetAllAsync();
                var hasUnreadNotifications = notifications.Any(a => !a.IsRead && a.UserId == id);
                return Ok(hasUnreadNotifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while checking for unread notifications.", details = ex.Message });
            }
        }
    }
}
