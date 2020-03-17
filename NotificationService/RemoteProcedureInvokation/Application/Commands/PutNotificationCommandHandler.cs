using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NotificationService.Model;

namespace NotificationService.Application.Commands
{
    public class PutNotificationCommandHandler : IRequestHandler<PutNotificationCommand, CommandReturnData>
    {
        NotificationContext _context;

        public PutNotificationCommandHandler(NotificationContext context)
        {
            _context = context;
        }

        public async Task<CommandReturnData> Handle(PutNotificationCommand request, CancellationToken cancellationToken)
        {
            var notificationLogs = _context.Notification_logs.ToList();

            var queries = notificationLogs.Where(x => x.Notification_id == request.Data.Attributes.Notification_id);

            foreach(var x in request.Data.Attributes.Target)
            {
                var data = queries.First(y => y.Target == x.Id).Id;
                var dataContext = await _context.Notification_logs.FindAsync(data);
                dataContext.Read_at = request.Data.Attributes.Read_at;
                await _context.SaveChangesAsync();   
            }

            return new CommandReturnData()
            {
                Message = "Data has been changed",
                Success = true
            };
        }
    }
}
