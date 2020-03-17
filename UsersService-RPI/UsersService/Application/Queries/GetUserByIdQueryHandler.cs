using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UsersService.Model;

namespace UsersService.Application.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdDTO>
    {
        private UserContext _context;

        public GetUserByIdQueryHandler(UserContext context)
        {
            _context = context;
        }

        public async Task<GetUserByIdDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Users.FindAsync(request.Id);

            return new GetUserByIdDTO()
            {
                Message = "Successfully Retrieving Data",
                Success = true,
                Data = data
            };
        }
    }
}
