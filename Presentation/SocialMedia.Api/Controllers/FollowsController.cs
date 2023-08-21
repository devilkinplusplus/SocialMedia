using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SocialMedia.Application.Features.Commands.Follow.AcceptRequest;
using SocialMedia.Application.Features.Commands.Follow.FollowUser;
using SocialMedia.Application.Features.Queries.Follow.FollowRequest;
using SocialMedia.Application.Features.Queries.Follow.MyFollowers;
using SocialMedia.Application.Features.Queries.Follow.MyFollowings;
using SocialMedia.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = nameof(RoleTypes.User))]
    [ApiController]
    public class FollowsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FollowsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(FollowUserCommandRequest request)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            request.FollowerId = id;

            var res = await _mediator.Send(request);
            return Ok(res);
        }
      
        [HttpGet("followings")]
        public async Task<IActionResult> Get([FromQuery]MyFollowingsQueryRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpGet("followers")]
        public async Task<IActionResult> GetFollowers([FromQuery]GetMyFollowersQueryRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpGet("followRequests")]
        public async Task<IActionResult> GetFollowRequests([FromQuery] FollowRequestQueryRequest request)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var id = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            request.Id = id;

            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> AcceptRequest(AcceptRequestCommandRequest request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

    }
}
