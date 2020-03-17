using System;
using UsersService.Model;

namespace UsersService.Application.Queries
{
    public class GetUserByIdDTO : BaseDTO
    {
        public User Data { get; set; }
    }
}
