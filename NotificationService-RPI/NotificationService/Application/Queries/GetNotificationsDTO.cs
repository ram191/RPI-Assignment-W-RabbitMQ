using System;
using System.Collections.Generic;
using NotificationService.Model;

namespace NotificationService.Application.Queries
{
    public class GetNotificationsDTO : BaseDTO
    {
        public List<NotificationData> Data { get; set; }
    }
}
