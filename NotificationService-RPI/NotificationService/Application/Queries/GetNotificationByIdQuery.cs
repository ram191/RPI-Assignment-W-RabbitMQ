using System;
using MediatR;

namespace NotificationService.Application.Queries
{
    public class GetNotificationByIdQuery : IRequest<GetNotificationByIdDTO>
    {
        public int Id { get; set; }

        public GetNotificationByIdQuery(int _id)
        {
            Id = _id;
        }
    }
}
