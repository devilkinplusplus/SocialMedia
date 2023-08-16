using SocialMedia.Application.DTOs.Follow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.Follow.FollowUser
{
    public class FollowUserCommandResponse : BaseCommandResponse
    {
        public FollowState FollowState { get; set; }
    }
}
