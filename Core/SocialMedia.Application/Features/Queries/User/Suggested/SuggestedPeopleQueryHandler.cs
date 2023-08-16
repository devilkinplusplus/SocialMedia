using MediatR;
using SocialMedia.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Features.Queries.User.Suggested
{
    public class SuggestedPeopleQueryHandler : IRequestHandler<SuggestedPeopleQueryRequest, SuggestedPeopleQueryResponse>
    {
        private readonly IUserService _userService;
        public SuggestedPeopleQueryHandler(IUserService userService) => _userService = userService;

        public async Task<SuggestedPeopleQueryResponse> Handle(SuggestedPeopleQueryRequest request, CancellationToken cancellationToken)
        {
            return await _userService.GetSuggestedPeopleAsync(request.UserId,request.Page, request.Size);
        }
    }
}
