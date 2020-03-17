using System;
using MediatR;

namespace UsersService.Application.Queries
{
    public class GetUserByIdQuery : IRequest<GetUserByIdDTO>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
