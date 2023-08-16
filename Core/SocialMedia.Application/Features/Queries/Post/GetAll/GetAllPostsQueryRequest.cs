﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.Post.GetAll
{
    public class GetAllPostsQueryRequest : IRequest<GetAllPostsQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
        public string? UserId { get; set; }
    }
}
