using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NotificationService.Model;

namespace NotificationService.Application.Commands
{
    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, CommandReturnData>
    {
        private NotificationContext _context;

        public DeleteNotificationCommandHandler(NotificationContext context)
        {
            _context = context;
        }

        public async Task<CommandReturnData> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var data = await _context.Notifications.FindAsync(request.Id);
            _context.Notifications.Remove(data);

            return new CommandReturnData()
            {
                Message = "Data has been deleted",
                Success = true
            };
        }
    }
}
