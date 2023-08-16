﻿using SocialMedia.Application.DTOs.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.Post.Create
{
    public class PostCreateCommandResponse : BaseCommandResponse
    {
        public PostListDto Post { get; set; }
    }
}
