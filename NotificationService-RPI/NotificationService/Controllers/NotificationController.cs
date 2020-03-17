using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Commands;
using NotificationService.Application.Queries;

namespace RemoteProcedureInvokation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {

        private IMediator _mediatr;

        public NotificationController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string include)
        {
            if(include == "logs")
            {
                var result = new GetNotificationsWLogsQuery();
                return Ok(await _mediatr.Send(result));
            }
            else
            {
                var result = new GetNotificationsQuery();
                return Ok(await _mediatr.Send(result));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id, string include)
        {
                var result = new GetNotificationByIdQuery(id);
                return Ok(await _mediatr.Send(result));
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostNotificationCommand data)
        {
            var result = await _mediatr.Send(data);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteNotificationCommand(id);
            var result = await _mediatr.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PutNotificationCommand data)
        {
            data.Data.Attributes.Notification_id = id;
            return Ok(await _mediatr.Send(data));
        }
    }
}
