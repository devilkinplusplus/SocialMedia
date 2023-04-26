using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.Follow.FollowRequest
{
    public class FollowRequestQueryRequest : IRequest<FollowRequestQueryResponse>
    {
        public string Id { get; set; }
    }
}
