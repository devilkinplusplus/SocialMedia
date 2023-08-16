using E = SocialMedia.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.User.Suggested
{
    public class SuggestedPeopleQueryResponse : BaseQueryListResponse<E.Suggested>
    {
        public int UserCount { get; set; }
    }
}
