using Moq;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.DTOs.User;
using SocialMedia.Application.Features.Commands.User.ChangePassword;
using SocialMedia.Application.Features.Commands.User.Create;
using SocialMedia.Application.Features.Commands.User.Edit;
using SocialMedia.Unit.Test.TestParameters.Users;
using Xunit;

namespace SocialMedia.Unit.Test.ServicesTests.Users
{
    public class UsersTests
    {
        [Theory]
        [ClassData(typeof(CreateUserParamaters))]
        public async Task CreateUserTestsAsync(CreateUserDto inputDto, CreateUserCommandResponse expected)
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            mockService
                .Setup(x => x.CreateUserAsync(It.IsAny<CreateUserDto>()))
                .ReturnsAsync(expected);

            // Act
            var actualResponse = await mockService.Object.CreateUserAsync(inputDto);

            // Assert
            Assert.Equal(expected.Succeeded, actualResponse.Succeeded);
        }

        [Theory]
        [ClassData(typeof(EditUserParameters))]
        public async Task EditUserTestsAsync(EditUserDto inputDto,EditUserCommandResponse expected)
        {
            var mockService = new Mock<IUserService>();
            mockService
                .Setup(x=>x.EditUserAsync(It.IsAny<EditUserDto>()))
                .ReturnsAsync(expected);

            var actual = await mockService.Object.EditUserAsync(inputDto);

            Assert.Equal(expected.Succeeded,actual.Succeeded);
        }

        [Theory]
        [ClassData(typeof(AssignRoleParameters))]
        public async Task AssignRoleTestsAsync(string userId,string roleName,bool expectedResult)
        {
            var mockService = new Mock<IUserService>();
            mockService
                .Setup(x=>x.AssignRoleAsync(userId,roleName))
                .ReturnsAsync(expectedResult);

            var actual = await mockService.Object.AssignRoleAsync(userId, roleName);

            Assert.Equal(expectedResult, actual);
        }

        [Theory]
        [ClassData(typeof(ChangePasswordParameters))]
        public async Task ChangePasswordTestsAsync(string userId,string newPassword, ChangePasswordCommandResponse expected)
        {
            var mockService = new Mock<IUserService>();

            mockService
                .Setup(x => x.ChangePasswordAsync(userId, newPassword))
                .ReturnsAsync(expected);

            var actual = await mockService.Object.ChangePasswordAsync(userId, newPassword);

            Assert.Equal(expected.Succeeded, actual.Succeeded);
        }
    
    
    
    }
}
