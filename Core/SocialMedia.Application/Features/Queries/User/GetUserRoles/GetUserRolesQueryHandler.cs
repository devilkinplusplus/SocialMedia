using MediatR;
using SocialMedia.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.User.GetUserRoles
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQueryRequest, GetUserRolesQueryResponse>
    {
        private readonly IUserService _userService;

        public GetUserRolesQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetUserRolesQueryResponse> Handle(GetUserRolesQueryRequest request, CancellationToken cancellationToken)
        {
           return await _userService.GetNonUserRolesAsync(request.UserId);
        }
    }
}
