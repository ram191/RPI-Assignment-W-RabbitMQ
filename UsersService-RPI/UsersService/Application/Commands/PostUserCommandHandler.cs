using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using UsersService.Model;
using RabbitMQ.Client;

namespace UsersService.Application.Commands
{
    public class PostUserCommandHandler : IRequestHandler<PostUserCommand, CommandReturnData>
    {
        private readonly UserContext _context;
        private static readonly HttpClient client = new HttpClient();

        public PostUserCommandHandler(UserContext context)
        {
            _context = context;
        }

        public async Task<CommandReturnData> Handle(PostUserCommand request, CancellationToken cancellationToken)
        {
            var data = new User()
            {
                Email = request.Data.Attributes.Email,
                Name = request.Data.Attributes.Name,
                Address = request.Data.Attributes.Address,
                Username = request.Data.Attributes.Username,
                Password = request.Data.Attributes.Password
            };

            _context.Add(data);

            await _context.SaveChangesAsync();

            var user = _context.Users.First(x => x.Username == request.Data.Attributes.Username);
            var target = new TargetCommand() { Id = user.Id, Email_destination = user.Email };

            PostCommand command = new PostCommand()
            {
                Title = "Welcome to the sample app",
                Message = "Please verify your identity by sending us your credit card security number",
                Type = "email",
                From = 1,
                Targets = new List<TargetCommand>() { target }
            };

            var attributes = new Data<PostCommand>()
            { Attributes = command };

            var httpContent = new CommandDTO<PostCommand>()
            { Data = attributes };

            var jObj = JsonConvert.SerializeObject(httpContent);

            //RabbitMq Producer
            var factory = new ConnectionFactory() { HostName = "some-rabbit" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("userDataExchange", "fanout");

                var body = Encoding.UTF8.GetBytes(jObj);

                channel.BasicPublish(
                    exchange: "",
                                routingKey: "userData",
                                basicProperties: null,
                                body: body
                                );
                Console.WriteLine("User data has been forwarded");
            }

            return new CommandReturnData()
            {
                Message = "New user has been added",
                Success = true
            };
        }
    }
}
