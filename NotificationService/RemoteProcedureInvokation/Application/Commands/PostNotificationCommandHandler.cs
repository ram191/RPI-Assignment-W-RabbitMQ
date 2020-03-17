using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NotificationService.Application.Queries;
using NotificationService.Model;

namespace NotificationService.Application.Commands
{
    public class PostNotificationCommandHandler : IRequestHandler<PostNotificationCommand, CommandReturnData>
    {
        private NotificationContext _context;

        public PostNotificationCommandHandler(NotificationContext context)
        {
            _context = context;
        }

        public async Task<CommandReturnData> Handle(PostNotificationCommand request, CancellationToken cancellationToken)
        {
            var notificationList = _context.Notifications.ToList();

            var notificationData = new Notification()
            {
                Message = request.Data.Attributes.Message,
                Title = request.Data.Attributes.Title
            };

            if (!notificationList.Any(x => x.Title == request.Data.Attributes.Title))
            {
                _context.Notifications.Add(notificationData);
            }
            await _context.SaveChangesAsync();
            var theNotification = _context.Notifications.First(x => x.Title == request.Data.Attributes.Title);
            //var notificationLogData = new List<NotificationLogs>();

            foreach (var x in request.Data.Attributes.Targets)
            {
                _context.Notification_logs.Add(new NotificationLogs
                {
                    Notification_id = theNotification.Id,
                    Type = request.Data.Attributes.Type,
                    From = request.Data.Attributes.From,
                    Target = x.Id,
                    Email_destination = x.Email_destination
                });
            }

            await _context.SaveChangesAsync();

            return new CommandReturnData()
            {
                Message = "Successfully Added",
                Success = true
            };
        }
    }
}
