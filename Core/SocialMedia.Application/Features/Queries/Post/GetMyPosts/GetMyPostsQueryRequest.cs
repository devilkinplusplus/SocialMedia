using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.Post.GetMyPosts
{
    public class GetMyPostsQueryRequest : IRequest<GetMyPostsQueryResponse>
    {
        public string? UserId { get; set; }
    }
}
