using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Features.Commands.Comment.Create
{
    public class CreateCommentCommandResponse: BaseCommandResponse
    {
        public E.Comment Comment { get; set; }
    }
}
