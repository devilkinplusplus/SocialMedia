using SocialMedia.Application.DTOs.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.Post.GetMyPosts
{
    public class GetMyPostsQueryResponse : BaseQueryListResponse<PostListDto>
    {
        public int PostCount { get; set; }
    }
}
