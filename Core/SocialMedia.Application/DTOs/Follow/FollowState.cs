using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.DTOs.Follow
{
    public class FollowState
    {
        public bool HasRequest { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsUnfollowed { get; set; }
        public Following Following { get; set; }
        public Following Follower { get; set; }
    }
}
