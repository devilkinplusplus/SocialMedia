using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.Post.Get
{
    public class GetPostCommandRequest : IRequest<GetPostCommandResponse>
    {
        public string PostId { get; set; }
    }
}
