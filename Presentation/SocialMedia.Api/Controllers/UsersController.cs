using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.Abstractions.Storage;
using SocialMedia.Application.Abstractions.Storage.Local;
using SocialMedia.Application.DTOs.User;
using SocialMedia.Application.Features.Commands.Auth.Login;
using SocialMedia.Application.Features.Commands.User.AssignRole;
using SocialMedia.Application.Features.Commands.User.ChangePassword;
using SocialMedia.Application.Features.Commands.User.ChangeVisibility;
using SocialMedia.Application.Features.Commands.User.Create;
using SocialMedia.Application.Features.Commands.User.Edit;
using SocialMedia.Application.Features.Commands.User.ResetPassword;
using SocialMedia.Application.Features.Commands.User.UploadProfileImage;
using SocialMedia.Application.Features.Queries.User.GetAll;
using SocialMedia.Application.Features.Queries.User.GetOne;
using SocialMedia.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer",Roles = nameof(RoleTypes.User))]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(CreateUserCommandRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommandRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost("changeVisibility")]
        public async Task<IActionResult> ChangeVisibility(ChangeVisibilityCommandRequest request)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            request.UserId = id;

            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(EditUserCommandRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadPICommandRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole(AssignRoleCommandRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllUsersQueryRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetOne(GetOneUserQueryRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost("resetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommandRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }
    }
}
