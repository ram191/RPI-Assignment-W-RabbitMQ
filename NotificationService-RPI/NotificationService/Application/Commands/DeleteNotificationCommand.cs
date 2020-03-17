using System;
using MediatR;

namespace NotificationService.Application.Commands
{
    public class DeleteNotificationCommand : IRequest<CommandReturnData>
    {
        public int Id { get; set; }

        public DeleteNotificationCommand(int id)
        {
            Id = id;
        }
    }
}
