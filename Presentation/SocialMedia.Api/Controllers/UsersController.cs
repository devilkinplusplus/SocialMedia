﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.Abstractions.Storage;
using SocialMedia.Application.Abstractions.Storage.Local;
using SocialMedia.Application.DTOs.User;
using SocialMedia.Application.Features.Commands.Auth.Login;
using SocialMedia.Application.Features.Commands.User.ChangePassword;
using SocialMedia.Application.Features.Commands.User.Create;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommandRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommandRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }


    }
}