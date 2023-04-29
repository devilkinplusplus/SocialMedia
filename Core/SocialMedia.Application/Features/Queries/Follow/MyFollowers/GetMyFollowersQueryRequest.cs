using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.Follow.MyFollowers
{
    public class GetMyFollowersQueryRequest:IRequest<GetMyFollowersQueryResponse>
    {
        public string Id { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
