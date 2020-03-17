using System;
using System.Collections.Generic;
using MediatR;
using NotificationService.Model;

namespace NotificationService.Application.Commands
{
    public class PutNotificationCommand : CommandDTO<PutCommand>, IRequest<CommandReturnData>
    {
    }

    public class PutCommand
    {
        public int Notification_id { get; set; }
        public DateTime Read_at { get; set; }
        public List<Target> Target { get; set; }
    }

    public class Target
    {
        public int Id { get; set; }
    }
}
