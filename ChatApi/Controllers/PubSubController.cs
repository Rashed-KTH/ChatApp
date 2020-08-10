using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NATS.Client;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ChatApi.Controllers
{
    [ApiController]
    [Route("api/message")]
    public class PubSubController : ControllerBase
    {
        private readonly ILogger<PubSubController> _logger;

        public PubSubController(ILogger<PubSubController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            string filePath = Path.Combine(Path.GetTempPath(), "message.txt");
            if(!System.IO.File.Exists(filePath))
            {
                return NoContent();
            }
            string message = await System.IO.File.ReadAllTextAsync(filePath);
            return message;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromQuery] string userName,
            [FromQuery] string subject)
        {
            string message = null;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {  
                message = await reader.ReadToEndAsync();
            }

            Options opts = ConnectionFactory.GetDefaultOptions();
            using (IConnection conn = new ConnectionFactory().CreateConnection(opts))
            {
                string dt = DateTime.Now.ToString(CultureInfo.GetCultureInfo("sv-SE"));
                string messageToPublish = $"[{userName}-{dt}] " + message;
                conn.Publish(subject, Encoding.Default.GetBytes(messageToPublish));
                conn.Flush();
            }
            return message;
        }
    }
}
