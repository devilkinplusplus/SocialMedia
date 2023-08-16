﻿using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Entities
{
    public class Rank : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<UserRank> UserRanks { get; set; }
        [NotMapped]
        public override DateTime Date { get => base.Date; set => base.Date = value; }
        [NotMapped]
        public override bool IsDeleted { get => base.IsDeleted; set => base.IsDeleted = value; }
    }
}
