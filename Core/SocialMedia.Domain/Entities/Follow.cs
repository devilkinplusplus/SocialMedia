using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Entities
{
    public class Follow : BaseEntity
    {
        //follower
        public User Follower { get; set; }
        public string FollowerId { get; set; }
        //following
        public User Following { get; set; }
        public string FollowingId { get; set; }
        public bool IsFollowing { get; set; }
        public bool HasRequest { get; set; }
        [NotMapped]
        public override bool IsDeleted { get => base.IsDeleted; set => base.IsDeleted = value; }
    }
}
