﻿using SocialMedia.Application.DTOs.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.Post.Edit
{
    public class EditPostCommandResponse : BaseCommandResponse
    {
        public PostListDto Post{ get; set; }
    }
}
