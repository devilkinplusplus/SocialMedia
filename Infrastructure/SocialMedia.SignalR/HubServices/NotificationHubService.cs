using Microsoft.AspNetCore.SignalR;
using SocialMedia.Application.Abstractions.Hubs;
using SocialMedia.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.SignalR.HubServices
{
    public class NotificationHubService:INotificationHubService
    {
        private readonly IHubContext<NotificationHub> _context;

        public NotificationHubService(IHubContext<NotificationHub> context)
        {
            _context = context;
        }
    }
}
