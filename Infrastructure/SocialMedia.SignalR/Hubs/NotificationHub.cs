using Microsoft.AspNetCore.SignalR;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.SignalR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.SignalR.Hubs
{
    public class NotificationHub : Hub
    {
        private static List<ConnectedUser> connectedUsers = new List<ConnectedUser>();
        private readonly IUserService _userService;

        public NotificationHub(IUserService userService)
        {
            _userService = userService;
        }

        public async Task SetConnectedUserAsync(string userId)
        {
            connectedUsers.Add(new ConnectedUser { ConnectionId = Context.ConnectionId, UserId = userId });
        }

        public async Task SendFollowNotificationAsync()
        {
            await Clients.All.SendAsync("followNotification");
        }

        public async Task SendLikeNotificationAsync(string userId)
        {
            string connectionId = connectedUsers.FirstOrDefault(x => x.UserId == userId).ConnectionId;
            if (connectionId is not null)
                await Clients.Client(connectionId).SendAsync("likeNotification");
        }

        public async Task SendCommentNotificationAsync(string userId)
        {
            string connectionId = connectedUsers.FirstOrDefault(x => x.UserId == userId).ConnectionId;
            if (connectionId is not null)
                await Clients.Client(connectionId).SendAsync("commentNotification");
        }

    }
}
