using SocialMedia.Application.DTOs.User;
using SocialMedia.Application.Features.Commands.User.ChangePassword;
using SocialMedia.Application.Features.Commands.User.Create;
using SocialMedia.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserCommandResponse> CreateUserAsync(CreateUserDto model);
        Task UpdateRefreshTokenAsync(string refreshToken, User user, DateTime accessTokenDate, int addOnAccessTokenDate);
        Task<ChangePasswordCommandResponse> ChangePasswordAsync(string userId, string newPassword);
    }
}
