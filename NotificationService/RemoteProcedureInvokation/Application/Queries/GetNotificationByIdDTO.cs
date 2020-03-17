using System;
using NotificationService.Model;

namespace NotificationService.Application.Queries
{
    public class GetNotificationByIdDTO : BaseDTO
    {
        public NotificationDTO Data { get; set; }
    }
}
