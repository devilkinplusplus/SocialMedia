using SocialMedia.Application.Repositories.PostReactions;
using SocialMedia.Domain.Entities;
using SocialMedia.Persistance.Contexts;

namespace SocialMedia.Persistance.Repositories.PostReactions
{
    public class PostReactionReadRepository : ReadRepository<PostReaction>, IPostReactionReadRepository
    {
        public PostReactionReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
