using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.Follow.AcceptRequest
{
    public class AcceptRequestCommandRequest : IRequest<AcceptRequestCommandResponse>
    {
        public string FollowerId { get; set; }
        public string? FollowingId { get; set; } //comes from token
    }
}
