using Microsoft.AspNetCore.Mvc;
using HamzaProject.Application.Interfaces;
using HamzaProject.Domain;
using System.Threading.Tasks;

namespace HamzaProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var message = await _messageService.GetMessageAsync();
            return Ok(message);
        }
    }
} 