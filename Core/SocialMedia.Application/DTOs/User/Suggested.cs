using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.DTOs.User
{
    public class Suggested
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ProfileImage{ get; set; }
        public bool IsFollowing { get; set; }
        public int FollowerCount { get; set; }
        public int FollowingCount { get; set; }
        public int PostCount { get; set; }
    }
}
