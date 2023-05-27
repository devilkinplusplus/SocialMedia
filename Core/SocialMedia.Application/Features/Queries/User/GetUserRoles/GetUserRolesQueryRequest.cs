using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.User.GetUserRoles
{
    public class GetUserRolesQueryRequest:IRequest<GetUserRolesQueryResponse>
    {
        public string? UserId { get; set; }
    }
}
