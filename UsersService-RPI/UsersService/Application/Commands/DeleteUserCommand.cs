using System;
using MediatR;

namespace UsersService.Application.Commands
{
    public class DeleteUserCommand : IRequest<CommandReturnData>
    {
        public int Id { get; set; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}
