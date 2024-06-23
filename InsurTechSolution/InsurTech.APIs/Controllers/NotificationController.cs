using AutoMapper;
using InsurTech.APIs.DTOs.NotificationDTO;
using InsurTech.Core;
using InsurTech.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsurTech.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetNotifications")]
        public async Task<IActionResult> GetNotifications()
        {
            try
            {
                var notifications = await _unitOfWork.Repository<Notification>().GetAllAsync();
                var notificationsDto = _mapper.Map<IEnumerable<GetNotificationDTO>>(notifications);
                return Ok(notificationsDto);
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

        [HttpGet("GetNotificationById/{id}")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            try
            {
                var notification = await _unitOfWork.Repository<Notification>().GetByIdAsync(id);
                if (notification == null)
                {
                    return NotFound(new { message = "Notification not found." });
                }
                var notificationDto = _mapper.Map<GetNotificationDTO>(notification);
                return Ok(notificationDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the notification.", details = ex.Message });
            }
        }

        [HttpGet("GetNotificationsByUserId/{userId}")]
        public async Task<IActionResult> GetNotificationsByUserId(string userId)
        {
            try
            {
                var notifications = await _unitOfWork.Repository<Notification>().GetAllAsync();
                var userNotifications = notifications.Where(a => a.UserId == userId).ToList();

                if (!userNotifications.Any())
                {
                    return NotFound(new { message = "Notifications not found for the user." });
                }

                var notificationsDto = _mapper.Map<IEnumerable<GetNotificationDTO>>(userNotifications);
                return Ok(notificationsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the notifications.", details = ex.Message });
            }
        }

        [HttpPost("MarkAllAsRead")]
        public async Task<IActionResult> MarkAllAsRead(string userId)
        {
            try
            {
                var notifications = await _unitOfWork.Repository<Notification>().GetAllAsync();
                var userNotifications = notifications.Where(a => a.UserId == userId && !a.IsRead).ToList();

                foreach (var notification in userNotifications)
                {
                    notification.IsRead = true;
                    _unitOfWork.Repository<Notification>().Update(notification);
                }

                await _unitOfWork.CompleteAsync();
                return Ok(new { message = "All notifications marked as read." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while marking notifications as read.", details = ex.Message });
            }
        }
    }
}
