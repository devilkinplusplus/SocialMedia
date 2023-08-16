using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Abstractions.Services
{
    public interface IPostReactionService
    {
        Task<bool> LikePostAsync(string userId,string postId);
        int GetPostReactions(string postId);
        Task<bool> IsAlreadyLikedAsync(string userId, string postId);
        bool IsAlreadyLiked(string userId, string postId);
    }
}
