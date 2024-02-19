using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NetCoreSignalRWebSocketImplementationAPI.Helpers;

namespace NetCoreSignalRWebSocketImplementationAPI.Controllers
{
    public class SignalRWebsocketController : ControllerBase
    {
        private IHubContext<MessageHub> _hubContext;

        public SignalRWebsocketController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [Route("api/test")]
        [HttpGet]
        [Authorize]
        public ActionResult Test()
        {
            return Ok("Test");
        }


        [Route("api/callHubTest")]
        [HttpPost]
        public async Task<IActionResult> CallHubMethod([FromQuery] string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            return Ok();
        }

    }
}
