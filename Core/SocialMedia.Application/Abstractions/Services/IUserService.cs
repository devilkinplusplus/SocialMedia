using SocialMedia.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(CreateUserDto model);
    }
}
