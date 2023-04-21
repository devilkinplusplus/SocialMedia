using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.Repositories.PostImages;
using SocialMedia.Application.Repositories.PostReactions;
using SocialMedia.Application.Repositories.Posts;
using SocialMedia.Application.Repositories.ProfileImages;
using SocialMedia.Domain.Entities.Identity;
using SocialMedia.Persistance.Contexts;
using SocialMedia.Persistance.Repositories.PostImages;
using SocialMedia.Persistance.Repositories.PostReactions;
using SocialMedia.Persistance.Repositories.Posts;
using SocialMedia.Persistance.Repositories.ProfileImages;
using SocialMedia.Persistance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(DbConfiguration.ConnectionString));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<AppDbContext>()
           .AddDefaultTokenProviders();

            services.AddHttpClient();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IPostReactionService, PostReactionService>();

            services.AddScoped<IProfileImageReadRepository, ProfileImageReadRepository>();
            services.AddScoped<IProfileImageWriteRepository, ProfileImageWriteRepository>();
            services.AddScoped<IPostReadRepository, PostReadRepository>();
            services.AddScoped<IPostWriteRepository, PostWriteRepository>();
            services.AddScoped<IPostImageReadRepository, PostImageReadRepository>();
            services.AddScoped<IPostImageWriteRepository, PostImageWriteRepository>();
            services.AddScoped<IPostReactionReadRepository, PostReactionReadRepository>();
            services.AddScoped<IPostReactionWriteRepository, PostReactionWriteRepository>();

        }
    }
}
