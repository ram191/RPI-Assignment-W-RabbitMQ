using System;
using System.Collections.Generic;
using NotificationService.Model;

namespace NotificationService.Application.Queries
{
    public class GetNotificationsWLogsDTO : BaseDTO
    {
        public List<NotificationDTO> Data { get; set; }
    }
}
