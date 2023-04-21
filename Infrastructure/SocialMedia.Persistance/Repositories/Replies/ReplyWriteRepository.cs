using SocialMedia.Application.Repositories.Replies;
using SocialMedia.Domain.Entities;
using SocialMedia.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Persistance.Repositories.Replies
{
    public class ReplyWriteRepository : WriteRepository<Reply>, IReplyWriteRepository
    {
        public ReplyWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
