using Microsoft.AspNetCore.SignalR;

namespace PharmaceuticalManagement_SonNN
{
    public class SignalrServer : Hub
    {
        private readonly ILogger<SignalrServer> _logger;

        public SignalrServer(ILogger<SignalrServer> logger)
        {
            _logger = logger;
        }

        public async Task TestMessage()
        {
            await Clients.All.SendAsync("TestEvent", "Hello from the server!");
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("A client connected with connection ID: {ConnectionId}", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("A client disconnected with connection ID: {ConnectionId}", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
