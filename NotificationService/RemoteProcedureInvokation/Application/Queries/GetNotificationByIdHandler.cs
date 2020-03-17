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
    public class GetNotificationByIdHandler : IRequestHandler<GetNotificationByIdQuery, GetNotificationByIdDTO>
    {
        private NotificationContext _context;

        public GetNotificationByIdHandler(NotificationContext context)
        {
            _context = context;
        }

        public async Task<GetNotificationByIdDTO> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notificationData =  await _context.Notifications.FirstAsync(x => x.Id == request.Id);
            var notificationLogData = await _context.Notification_logs.Where(x => x.Notification_id == request.Id).ToListAsync();

            var logList = new List<NotificationLogData>();

            foreach(var x in notificationLogData)
            {
                logList.Add(new NotificationLogData()
                {
                    Notification_id = x.Notification_id,
                    From = x.From,
                    Read_at = x.Read_at,
                    Target = x.Target
                });
            }

            return new GetNotificationByIdDTO()
            {
                Message = "Successfully retrieving data",
                Success = true,
                Data = new NotificationDTO()
                {
                    Notifications = new NotificationData()
                    {
                        Id = notificationData.Id,
                        Title = notificationData.Title,
                        Message = notificationData.Message
                    },
                    Notification_logs = logList
                }
            };
        }
    }
}
