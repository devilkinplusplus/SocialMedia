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
            var res = await _userService.UploadProfileImageAsync(request.UserId,request.File);
            return new() { Succeeded = res };
        }
    }
}
