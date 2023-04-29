using SocialMedia.Application.DTOs.Follow;

namespace SocialMedia.Application.Abstractions.Services
{
    public interface IFollowService
    {
        Task FollowUserAsync(string followerId,string followingId);
        Task<FollowingDto> GetMyFollowingsAsync(string id,int page = 0,int size = 5);
        Task<FollowingDto> GetMyFollowersAsync(string id,int page = 0,int size = 5);
        Task<FollowingDto> GetMyFollowRequestsAsync(string id, int page = 0, int size = 5);
        Task AcceptFollowRequestAsync(string followerId,string followingId);

    }
}
