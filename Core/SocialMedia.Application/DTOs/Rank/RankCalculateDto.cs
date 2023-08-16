using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.DTOs.Rank
{
    public class RankCalculateDto
    {
        public int Posts { get; set; }
        public int Likes { get; set; }
        public int Followers { get; set; }
        public int Followings { get; set; }
    }
}
