using System;
using MediatR;

namespace NotificationService.Application.Queries
{
    public class GetNotificationsQuery : IRequest<GetNotificationsDTO>
    {
    }
}
