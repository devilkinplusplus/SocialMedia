using SocialMedia.Application.Repositories.Follows;
using SocialMedia.Domain.Entities;
using SocialMedia.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Persistance.Repositories.Follows
{
    public class FollowReadRepository : ReadRepository<Follow>, IFollowReadRepository
    {
        public FollowReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
