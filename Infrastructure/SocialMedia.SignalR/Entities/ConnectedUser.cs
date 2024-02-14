using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.SignalR.Entities
{
    public class ConnectedUser
    {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}
