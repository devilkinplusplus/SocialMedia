using SocialMedia.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Entities
{
    public class UserRank
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string RankId { get; set; }
        public Rank Rank { get; set; }
    }
}
