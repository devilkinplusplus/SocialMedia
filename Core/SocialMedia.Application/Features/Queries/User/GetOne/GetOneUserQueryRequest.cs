using MediatR;
using SocialMedia.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.User.GetOne
{
    public class GetOneUserQueryRequest : IRequest<GetOneUserQueryResponse>
    {
        public string? FollowingId { get; set; }
        public string? FollowerId { get; set; }
        public string UserId { get; set; }
    }
}
