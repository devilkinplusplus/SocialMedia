using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.DTOs.User;
using SocialMedia.Application.Validators;
using SocialMedia.Domain.Entities.Identity;
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

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CreateUserAsync(CreateUserDto model)
        {
            UserValidator validations = new();
            User user = new()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                Date = model.Date,
            };

            ValidationResult vResults = validations.Validate(user);
            if (vResults.IsValid)
            {
                IdentityResult result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                    return true;
            }


            return false;
        }
    }
}
