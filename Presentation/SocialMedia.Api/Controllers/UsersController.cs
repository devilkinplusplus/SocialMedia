using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.DTOs.User;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userservice;

        public UsersController(IUserService userservice)
        {
            _userservice = userservice;
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateUserDto model)
        {
            bool res = await _userservice.CreateUserAsync(model);
            return Ok(res);
        }
    }
}
