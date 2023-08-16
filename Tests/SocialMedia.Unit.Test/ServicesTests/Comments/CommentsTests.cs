using Moq;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.DTOs.Comment;
using SocialMedia.Application.Features.Commands.Comment.Create;
using SocialMedia.Unit.Test.TestParameters.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Unit.Test.ServicesTests.Comments
{
    public class CommentsTests
    {
        [Theory]
        [ClassData(typeof(CreateCommentParameters))]
        public async Task CreateCommentTestsAsync(CreateCommentDto inputDto,CreateCommentCommandResponse expected)
        {
            //var mockService = new Mock<ICommentService>();

            //mockService
            //    .Setup(x => x.CreateCommentAsync(It.IsAny<CreateCommentDto>()))
            //    .ReturnsAsync(expected);

            //var actual = await mockService.Object.CreateCommentAsync(inputDto);

            //Assert.Equal(expected.Succeeded, actual.Succeeded);
        }


    }
}
