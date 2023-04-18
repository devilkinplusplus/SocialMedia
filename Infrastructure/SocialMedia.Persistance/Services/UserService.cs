using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.DTOs.User;
using SocialMedia.Application.Features.Commands.User.ChangePassword;
using SocialMedia.Application.Features.Commands.User.Create;
using SocialMedia.Application.Validators;
using SocialMedia.Domain.Entities.Identity;
using SocialMedia.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Persistance.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordValidator<User> _passwordValidator;
        public UserService(UserManager<User> userManager, AppDbContext context, IMapper mapper, IPasswordValidator<User> passwordValidator)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _passwordValidator = passwordValidator;
        }

        public async Task<ChangePasswordCommandResponse> ChangePasswordAsync(string userId, string newPassword)
        {
            User? user = await _userManager.FindByIdAsync(userId);

            if (user is not null)
            {
                string newHashedPassword = _userManager.PasswordHasher.HashPassword(user, newPassword);

                user.PasswordHash = newHashedPassword;

                IdentityResult passwordResult = await _passwordValidator.ValidateAsync(_userManager, user, newPassword);

                if (passwordResult.Succeeded)
                {
                    await UpdatePasswordAsync(user);
                    return new() { Succeeded = true };
                }
                return new() { Succeeded = false, Errors = passwordResult.Errors.Select(x => x.Description).ToList() };

            }

            return new() { Succeeded = false };
        }

        public async Task<CreateUserCommandResponse> CreateUserAsync(CreateUserDto model)
        {
            UserValidator validations = new();
            User user = _mapper.Map<User>(model);
            user.Id = Guid.NewGuid().ToString();

            ValidationResult vResults = validations.Validate(user);

            if (await IsEmailExist(user.Email))
                return new() { Succeeded = false, Errors = new() { "Email already in use" } };

            if (vResults.IsValid)
            {
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return new() { Succeeded = true };
                return new() { Succeeded = false, Errors = result.Errors.Select(x => x.Description).ToList() };
            }

            return new() { Succeeded = false, Errors = vResults.Errors.Select(x => x.ErrorMessage).ToList() };
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, User user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user is not null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddMinutes(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new Exception("Xeta");
        }

        private async Task<bool> IsEmailExist(string email) => await _context.Users.AnyAsync(x => x.Email == email);

        private async Task UpdatePasswordAsync(User user) => await _userManager.UpdateAsync(user);

    }
}
