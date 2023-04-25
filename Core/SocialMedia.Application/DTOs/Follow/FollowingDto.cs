using SocialMedia.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.DTOs.Follow
{
    public class FollowingDto
    {
        public int FollowingCount { get; set; }
        public List<Following> Followings { get; set; }
    }
}
