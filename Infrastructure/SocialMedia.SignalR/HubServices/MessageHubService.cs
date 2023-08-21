using Microsoft.AspNetCore.SignalR;
using SocialMedia.Application.Abstractions.Hubs;
using SocialMedia.Domain.Entities.Identity;
using SocialMedia.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.SignalR.HubServices
{
    public class MessageHubService : IMessageHubService
    {
        private readonly IHubContext<ChatHub> _context;
        public MessageHubService(IHubContext<ChatHub> context)
        {
            _context = context;
        }

       

    }
}
