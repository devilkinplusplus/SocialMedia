﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.PostReaction.Like
{
    public class LikePostCommandResponse : BaseCommandResponse
    {
        public bool IsLike { get; set; }
    }
}
