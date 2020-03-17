using System;
using MediatR;

namespace UsersService.Application.Queries
{
    public class GetUsersQuery : IRequest<GetUsersDTO>
    { 
    }
}
