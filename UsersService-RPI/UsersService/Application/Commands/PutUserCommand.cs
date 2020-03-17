using System;
using MediatR;
using UsersService.Model;

namespace UsersService.Application.Commands
{
    public class PutUserCommand : CommandDTO<User>, IRequest<CommandReturnData>
    {
    }
}
