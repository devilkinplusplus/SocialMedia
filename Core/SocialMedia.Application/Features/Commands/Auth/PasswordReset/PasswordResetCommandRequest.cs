using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.Auth.PasswordReset
{
    public class PasswordResetCommandRequest :IRequest<PasswordResetCommandResponse>
    {
        public string Email { get; set; }
    }
}
