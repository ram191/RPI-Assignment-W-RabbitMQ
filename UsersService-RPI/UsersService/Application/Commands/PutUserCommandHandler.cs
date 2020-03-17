using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UsersService.Model;

namespace UsersService.Application.Commands
{
    public class PutUserCommandHandler : IRequestHandler<PutUserCommand, CommandReturnData>
    {
        private UserContext _context;

        public PutUserCommandHandler(UserContext context)
        {
            _context = context;
        }

        public async Task<CommandReturnData> Handle(PutUserCommand request, CancellationToken cancellationToken)
        {
            var data = await _context.Users.FindAsync(request.Data.Attributes.Id);

            data.Name = request.Data.Attributes.Name;
            data.Address = request.Data.Attributes.Address;
            data.Email = request.Data.Attributes.Email;
            data.Username = request.Data.Attributes.Username;
            data.Password = request.Data.Attributes.Password;

            _context.SaveChanges();

            return new CommandReturnData()
            {
                Message = "Data has been modified",
                Success = true
            };
        }
    }
}
