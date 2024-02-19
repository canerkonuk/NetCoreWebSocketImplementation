using Microsoft.AspNetCore.SignalR;

namespace NetCoreSignalRImplementationWebApp.Hubs
{
    public class BroadcastMessage : Hub
    {
        private static Timer _timer;
        private readonly IHubContext<BroadcastMessage> _hubContext;

        public BroadcastMessage(IHubContext<BroadcastMessage> hubContext)
        {
            _hubContext = hubContext;

            if (_timer == null)
            {
                _timer = new Timer(SendMessage, null, 0, 1000); // Sends a message every 1 second
            }
        }

        private void SendMessage(object state)
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "This is a continuous message");
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _timer?.Dispose();
            _timer = null;
            return base.OnDisconnectedAsync(exception);
        }
    }

}
