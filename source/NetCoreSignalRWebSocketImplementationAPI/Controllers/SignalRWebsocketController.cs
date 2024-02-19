using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreSignalRWebSocketImplementationAPI.Controllers
{
    public class SignalRWebsocketController : ControllerBase
    {
        [Route("api/test")]
        [HttpGet]
        [Authorize]
        public ActionResult Test()
        {
            return Ok("Test");
        }
    }
}
