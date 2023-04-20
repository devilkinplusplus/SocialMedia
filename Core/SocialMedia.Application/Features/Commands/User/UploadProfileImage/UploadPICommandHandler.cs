using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialMedia.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Commands.User.UploadProfileImage
{
    public class UploadPICommandHandler : IRequestHandler<UploadPICommandRequest, UploadPICommandResponse>
    {
        private readonly IUserService _userService;
        public UploadPICommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UploadPICommandResponse> Handle(UploadPICommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.UploadProfileImageAsync(request.Files);
            return new() { Succeeded = true };
        }
    }
}
