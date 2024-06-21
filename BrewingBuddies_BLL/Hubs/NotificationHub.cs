using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace BrewingBuddies_BLL.Hubs
{
    //[Authorize]
    public class NotificationHub : Hub
    {

        //public override Task OnConnectedAsync()
        //{
        //    // Handle connection logic
        //    return base.OnConnectedAsync();
        //}
        //public async Task SendMessage(string userId, string message)
        //{
        //    await Clients.User(userId).SendAsync("ReceiveMessage", message);
        //    //await Clients.User(senderId).SendAsync("FriendRequestAccepted");
        //}
    }
}
