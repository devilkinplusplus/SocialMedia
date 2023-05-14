using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialMedia.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.User.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, ResetPasswordCommandResponse>
    {
        private readonly IUserService _userService;
        public ResetPasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResetPasswordCommandResponse> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.UpdatePasswordAsync(request.UserId, request.NewPassword, request.ResetToken);
            return new() { Succeeded  = true };
        }
    }
}
