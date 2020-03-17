using System;
using System.Collections.Generic;
using UsersService.Model;

namespace UsersService.Application.Queries
{
    public class GetUsersDTO : BaseDTO
    {
        public List<User> Data { get; set; }
    }
}
