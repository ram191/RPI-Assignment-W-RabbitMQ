using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UsersService.Model;

namespace UsersService.Application.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersDTO>
    {
        private UserContext _context;

        public GetUsersQueryHandler(UserContext context)
        {
            _context = context;
        }

        public async Task<GetUsersDTO> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var userData = await _context.Users.ToListAsync();

            return new GetUsersDTO()
            {
                Message = "Successfully retrieving data",
                Success = true,
                Data = userData
            };
        }
    }
}
