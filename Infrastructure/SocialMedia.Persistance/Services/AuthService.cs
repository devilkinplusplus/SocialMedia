using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.Abstractions.Token;
using SocialMedia.Application.DTOs;
using SocialMedia.Application.Features.Commands.Auth.Login;
using SocialMedia.Domain.Entities.Identity;
using SocialMedia.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Persistance.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;
        private readonly AppDbContext _context;
        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenHandler tokenHandler, IUserService userService, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
            _context = context;
        }

        public async Task<LoginCommandResponse> LoginAsync(string emailOrUsername, string password)
        {
            User user = await _userManager.FindByEmailAsync(emailOrUsername);

            if (user is null)
                user = await _userManager.FindByNameAsync(emailOrUsername);

            if (user is null)
                return new() { Succeeded = false, Errors = new List<string> { "Username or email is incorrect" } };

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                Token token = await _tokenHandler.CreateTokenAsync(user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 10);
                return new() { Succeeded = true, Token = token };
            }

            return new() { Succeeded = false, Errors = new List<string> { "Password is incorrect" } };
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

            if (user is not null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = await _tokenHandler.CreateTokenAsync(user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 10);
                return token;
            }
            else
                throw new Exception("message");
        }
    }
}
