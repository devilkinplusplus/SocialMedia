using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Abstractions.Hubs;
using SocialMedia.SignalR.HubServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddTransient<IMessageHubService, MessageHubService>();
            services.AddTransient<INotificationHubService, NotificationHubService>();
        }
    }
}
