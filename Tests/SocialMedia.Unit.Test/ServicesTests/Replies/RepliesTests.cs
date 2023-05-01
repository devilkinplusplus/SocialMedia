using Moq;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.DTOs.Reply;
using SocialMedia.Application.Features.Commands.Reply.Create;
using SocialMedia.Unit.Test.TestParameters.Replies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Unit.Test.ServicesTests.Replies
{
    public class RepliesTests
    {
        [Theory]
        [ClassData(typeof(CreateReplyParameters))]
        public async Task CreateRepliesTestsAsync(CreateReplyDto inputDto,CreateReplyCommandResponse expected)
        {
            var mockService = new Mock<IReplyService>();

            mockService
                .Setup(x => x.CreateReplyAsync(It.IsAny<CreateReplyDto>()))
                .ReturnsAsync(expected);

            var actual = await mockService.Object.CreateReplyAsync(inputDto);

            Assert.Equal(expected.Succeeded, actual.Succeeded);
        }

    }
}
