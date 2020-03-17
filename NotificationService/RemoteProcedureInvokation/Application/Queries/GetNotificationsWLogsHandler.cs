using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NotificationService.Model;

namespace NotificationService.Application.Queries
{
    public class GetNotificationsWLogsHandler : IRequestHandler<GetNotificationsWLogsQuery, GetNotificationsWLogsDTO>
    {
        private NotificationContext _context;

        public GetNotificationsWLogsHandler(NotificationContext context)
        {
            _context = context;
        }

        public async Task<GetNotificationsWLogsDTO> Handle(GetNotificationsWLogsQuery request, CancellationToken cancellationToken)
        {
            var notificationData = await _context.Notifications.ToListAsync();
            var notificationLogData = await _context.Notification_logs.ToListAsync();

            var notifList = new List<NotificationDTO>();

            foreach(var x in notificationData)
            {
                var logList = new List<NotificationLogData>();
                var logs = notificationLogData.Where(y => y.Notification_id == x.Id);
                foreach(var y in logs)
                {
                    logList.Add(new NotificationLogData
                    {
                       Notification_id = y.Notification_id,
                       From = y.From,
                       Read_at = y.Read_at,
                       Target = y.Target
                    });
                }
                notifList.Add(new NotificationDTO()
                {
                    Notifications = new NotificationData()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Message = x.Message
                    },
                    Notification_logs = logList
                });
            }

            return new GetNotificationsWLogsDTO()
            {
                Message = "Successfully retrieving data",
                Success = true,
                Data = notifList
            };
        }
    }
}
