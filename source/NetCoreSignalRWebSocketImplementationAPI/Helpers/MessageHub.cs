using Microsoft.AspNetCore.SignalR;

namespace NetCoreSignalRWebSocketImplementationAPI.Helpers
{
    public class MessageHub: Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", user, message);
            //All, Caller, Others
        }
    }
}
