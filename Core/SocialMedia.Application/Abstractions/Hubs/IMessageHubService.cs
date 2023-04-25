using SocialMedia.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Abstractions.Hubs
{
    public interface IMessageHubService
    {
        Task SendMessageAsync(string message, User user);
    }
}
