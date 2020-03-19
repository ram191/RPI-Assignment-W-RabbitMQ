using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MimeKit;
using Newtonsoft.Json;
using NotificationService.Application.Queries;
using NotificationService.Model;

namespace NotificationService.Application.Commands
{
    public class PostNotificationCommandHandler : IRequestHandler<PostNotificationCommand, CommandReturnData>
    {
        private NotificationContext _context;

        public PostNotificationCommandHandler(NotificationContext context)
        {
            _context = context;
        }

        public async Task<CommandReturnData> Handle(PostNotificationCommand request, CancellationToken cancellationToken)
        {
            //Adding Notification to Db
            var notificationList = _context.Notifications.ToList();

            var notificationData = new Notification()
            {
                Message = request.Data.Attributes.Message,
                Title = request.Data.Attributes.Title
            };

            if (!notificationList.Any(x => x.Title == request.Data.Attributes.Title))
            {
                _context.Notifications.Add(notificationData);
            }
            await _context.SaveChangesAsync();

            //Adding Notification_log to Db
            var theNotification = _context.Notifications.First(x => x.Title == request.Data.Attributes.Title);
            foreach (var x in request.Data.Attributes.Targets)
            {
                //var users = GetUserData().Result;
                //var senderMail = users.Where(x => x.Id == request.Data.Attributes.From).Select(x => x.Email).ToString();
                _context.Notification_logs.Add(new NotificationLogs
                {
                    Notification_id = theNotification.Id,
                    Type = request.Data.Attributes.Type,
                    From = request.Data.Attributes.From,
                    Target = x.Id,
                    Email_destination = x.Email_destination
                });

                await _context.SaveChangesAsync();
                await SendMail("admin@admin.com", x.Email_destination, request.Data.Attributes.Title, request.Data.Attributes.Message);
            }

            await _context.SaveChangesAsync();

            return new CommandReturnData()
            {
                Message = "Successfully Added",
                Success = true
            };
        }

        //Getting User Data
        public async Task<List<User>> GetUserData()
        {
            var client = new HttpClient();
            var data = await client.GetStringAsync("http://localhost:4000/users");
            return JsonConvert.DeserializeObject<List<User>>(data);
        }

        //Sending notification email
        public async Task SendMail(string emailfrom, string emailto, string subject, string body)
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("4101aedaf3b46c", "e05b5c377ba6d8"),
                EnableSsl = true
            };
            await client.SendMailAsync(emailfrom, emailto, subject, body);
            Console.WriteLine("Email has been sent");
        }
    }
}
