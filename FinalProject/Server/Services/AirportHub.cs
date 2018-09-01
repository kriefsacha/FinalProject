using Microsoft.AspNet.SignalR;

namespace Server.Services
{
    /// <summary>
    /// The hub of the airport (we don't put functions because we don't want the user to be capable of calling them , we want to decide when it fires
    /// </summary>
    public class AirportHub : Hub
    {
    }
}