using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UsersService.Model;

namespace UsersService.Application.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, CommandReturnData>
    {
        private UserContext _context;

        public DeleteUserCommandHandler(UserContext context)
        {
            _context = context;
        }

        public async Task<CommandReturnData> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var data = await _context.Users.FindAsync(request.Id);
            _context.Users.Remove(data);
            _context.SaveChanges();

            return new CommandReturnData()
            {
                Message = "Data has been deleted",
                Success = true
            };
        }
    }
}
