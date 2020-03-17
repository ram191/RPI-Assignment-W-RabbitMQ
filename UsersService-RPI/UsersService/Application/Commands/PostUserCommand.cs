using System;
using MediatR;
using UsersService.Model;

namespace UsersService.Application.Commands
{
    public class PostUserCommand : CommandDTO<User>, IRequest<CommandReturnData>
    {
    }
}
