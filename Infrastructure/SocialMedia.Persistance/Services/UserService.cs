using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.Abstractions.Storage;
using SocialMedia.Application.DTOs.User;
using SocialMedia.Application.Features.Commands.User.ChangePassword;
using SocialMedia.Application.Features.Commands.User.ChangeVisibility;
using SocialMedia.Application.Features.Commands.User.Create;
using SocialMedia.Application.Features.Commands.User.Edit;
using SocialMedia.Application.Repositories.ProfileImages;
using SocialMedia.Application.Validators;
using SocialMedia.Domain.Entities.Identity;
using SocialMedia.Domain.Exceptions;
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
        private readonly IStorageService _storageService;
        private readonly IProfileImageWriteRepository _profileImageWrite;
        public UserService(UserManager<User> userManager, AppDbContext context, IMapper mapper, IPasswordValidator<User> passwordValidator, IStorageService storageService, IProfileImageWriteRepository profileImageWrite)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _passwordValidator = passwordValidator;
            _storageService = storageService;
            _profileImageWrite = profileImageWrite;
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
                    await UpdateUserAsync(user);
                    return new() { Succeeded = true };
                }
                return new() { Succeeded = false, Errors = passwordResult.Errors.Select(x => x.Description).ToList() };

            }

            return new() { Succeeded = false };
        }

        public async Task<ChangeVisibilityCommandResponse> ChangeVisibilityAsync(string userId)
        {
            User? user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                user.IsPrivate = !user.IsPrivate;
                await UpdateUserAsync(user);
                return new() { Succeeded = true };
            }
            throw new UserNotFoundException();
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

        public async Task<EditUserCommandResponse> EditUserAsync(EditUserDto model)
        {
            User? user = await _userManager.FindByIdAsync(model.Id);
            if (user is not null)
            {
                user.FirstName = (model.FirstName is null ? user.FirstName : model.FirstName);
                user.LastName = (model.LastName is null ? user.LastName : model.LastName);
                user.UserName = (model.UserName is null ? user.UserName : model.UserName);
                user.About = (model.About is null ? user.About : model.About);
                user.Date = model.Date;


                ValidationResult vResult = await ValidateUserAsync(user);
                if (vResult.IsValid)
                {
                    IdentityResult res = await UpdateUserAsync(user);
                    return new() { Succeeded = res.Succeeded, Errors = res.Errors.Select(x => x.Description).ToList() };
                }
                return new() { Succeeded = vResult.IsValid, Errors = vResult.Errors.Select(x => x.ErrorMessage).ToList() };

            }
            throw new UserNotFoundException();
        }

        private async Task<ValidationResult> ValidateUserAsync(User user)
        {
            UserValidator validationRules = new();
            ValidationResult result = await validationRules.ValidateAsync(user);
            return result;
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
                throw new UserNotFoundException();
        }

        private async Task<bool> IsEmailExist(string email) => await _context.Users.AnyAsync(x => x.Email == email);

        private async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task UploadProfileImageAsync(IFormFileCollection files)
        {
            string pathName = "uploads\userImages";
            var storageInfo = await _storageService.UploadAsync(pathName, files);

            foreach (var item in storageInfo)
            {
                string fullPath = $"{item.pathName}/{item.fileName}";
                await _profileImageWrite.AddAsync(new()
                {
                    Id = Guid.NewGuid().ToString(),
                    FileName = item.fileName,
                    Path = fullPath,
                    Storage = _storageService.StorageName,
                });
            }
            await _profileImageWrite.SaveAsync();
        }
    }
}
