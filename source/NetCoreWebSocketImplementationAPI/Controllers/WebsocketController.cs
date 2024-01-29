using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Net.WebSockets;
using System.Text;

namespace NetCoreWebSocketImplementationAPI.Controllers
{
    [Authorize]
    [ApiController]
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


        [Route("api/wsContinuous")]
        [HttpGet]
        public async Task WebsocketContinuousMessage()
        {

            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                var message = "Test message from server";
                var bytes = Encoding.UTF8.GetBytes(message);

                while (webSocket.State == WebSocketState.Open)
                {
                    await webSocket.SendAsync(
                        new ArraySegment<byte>(bytes),
                        WebSocketMessageType.Text,
                        endOfMessage: true,
                        cancellationToken: CancellationToken.None);

                    // Delay to prevent high CPU usage
                    await Task.Delay(1000);
                }
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }



        [Route("api/wsTest")]
        [HttpGet]
        public async Task WebsocketTestMessage()
        {

            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                var buffer = new byte[1024 * 4];
                var receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);

                while (!receiveResult.CloseStatus.HasValue)
                {
                    var message = "Test message from server";
                    var bytes = Encoding.UTF8.GetBytes(message);

                    await webSocket.SendAsync(
                        new ArraySegment<byte>(bytes),
                        receiveResult.MessageType,
                        receiveResult.EndOfMessage,
                        CancellationToken.None);

                    receiveResult = await webSocket.ReceiveAsync(
                        new ArraySegment<byte>(buffer), CancellationToken.None);
                }

                await webSocket.CloseAsync(
                    receiveResult.CloseStatus.Value,
                    receiveResult.CloseStatusDescription,
                    CancellationToken.None);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }


        [Route("api/wsEcho")]
        [HttpGet]
        public async Task WebsocketEchoMessage()
        {

            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                var buffer = new byte[1024 * 4];
                var receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);

                while (!receiveResult.CloseStatus.HasValue)
                {
                    await webSocket.SendAsync(
                        new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                        receiveResult.MessageType,
                        receiveResult.EndOfMessage,
                        CancellationToken.None);

                    receiveResult = await webSocket.ReceiveAsync(
                        new ArraySegment<byte>(buffer), CancellationToken.None);
                }

                await webSocket.CloseAsync(
                    receiveResult.CloseStatus.Value,
                    receiveResult.CloseStatusDescription,
                    CancellationToken.None);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }

        }
    }
}