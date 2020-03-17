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
    public class GetNotificationsHandler : IRequestHandler<GetNotificationsQuery, GetNotificationsDTO>
    {
        private readonly NotificationContext _context;

        public GetNotificationsHandler(NotificationContext context)
        {
            _context = context;
        }

        public async Task<GetNotificationsDTO> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notificationData = await _context.Notifications.ToListAsync();
            var result = new List<NotificationData>();

            foreach(var x in notificationData)
            {
                result.Add(new NotificationData
                {
                    Id = x.Id,
                    Title = x.Title,
                    Message = x.Message
                });
            }

            return new GetNotificationsDTO
            {
                Message = "Success retrieving data",
                Success = true,
                Data = result
            };
        }

        

    }
}
