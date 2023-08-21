using Microsoft.AspNetCore.Http;
using Moq;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.DTOs.Post;
using SocialMedia.Application.Features.Commands.Post.Create;
using SocialMedia.Application.Features.Commands.Post.Edit;
using SocialMedia.Domain.Entities;
using SocialMedia.Unit.Test.TestParameters.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Unit.Test.ServicesTests.Posts
{
    public class PostsTests
    {

        [Theory]
        [ClassData(typeof(CreatePostParameters))]
        public async Task CreatePostTestsAsync(CreatePostDto inputDto, PostCreateCommandResponse expectedResponse)
        {
            // Arrange
            var mockPostService = new Mock<IPostService>();
            mockPostService
                .Setup(x => x.CreatePostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IFormFileCollection>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var actualResponse = await mockPostService.Object.CreatePostAsync(inputDto.Content,inputDto.UserId,inputDto.Files);

            // Assert
            Assert.Equal(expectedResponse.Succeeded, actualResponse.Succeeded);
        }

        [Theory]
        [ClassData(typeof(EditPostParameters))]
        public async Task EditPostTestsAsync(EditPostDto inputDto,EditPostCommandResponse expextedResponse)
        {
            var mockPostService = new Mock<IPostService>();
            mockPostService.Setup(x => x.EditPostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IFormFileCollection>()))
                .ReturnsAsync(expextedResponse);

            var actualResponse = await mockPostService.Object.EditPostAsync(inputDto.Id,inputDto.Content,inputDto.Files);

            Assert.Equal(expextedResponse.Succeeded, actualResponse.Succeeded);
        }
    }
}
