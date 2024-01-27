using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace NetCoreWebSocketImplementationAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class WebsocketController : ControllerBase
    {
        
        private readonly ILogger<WebsocketController> _logger;

        public WebsocketController(ILogger<WebsocketController> logger)
        {
            _logger = logger;
        }

        [Route("api/test")]
        [HttpGet]
        public ActionResult Test()
        {
            return Ok("Test");
        }
    }
}