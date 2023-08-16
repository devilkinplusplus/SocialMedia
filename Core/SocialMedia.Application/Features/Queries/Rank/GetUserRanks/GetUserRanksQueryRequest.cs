using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.Rank.GetUserRanks
{
    public class GetUserRanksQueryRequest : IRequest<GetUserRanksQueryResponse>
    {
        public string UserId { get; set; }
    }
}
