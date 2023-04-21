using SocialMedia.Application.Repositories.PostReactions;
using SocialMedia.Domain.Entities;
using SocialMedia.Persistance.Contexts;

namespace SocialMedia.Persistance.Repositories.PostReactions
{
    public class PostReactionWriteRepository : WriteRepository<PostReaction>, IPostReactionWriteRepository
    {
        public PostReactionWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
