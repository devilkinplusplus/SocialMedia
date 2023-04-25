using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.Follow.FollowUser
{
    public class FollowUserCommandRequest : IRequest<FollowUserCommandResponse>
    {
        public string? FollowerId { get; set; }
        public string FollowingId { get; set; }
    }
}
