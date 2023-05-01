using SocialMedia.Application.DTOs.User;
using SocialMedia.Application.Features.Commands.User.Create;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Unit.Test.TestParameters.Users
{
    public class CreateUserParamaters : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new CreateUserDto
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Email= "test@gmail.com",
                    UserName = "TestMate",
                    Password = "strongpass1",
                    Date = DateTime.UtcNow,
                },
                new CreateUserCommandResponse(){ Succeeded = true }
            };
            yield return new object[]
            {
                new CreateUserDto
                {
                    FirstName = null,
                    LastName = null,
                    Email= "test@gmail.com",
                    UserName = "TestMate",
                    Password = "strongpass1",
                    Date = DateTime.UtcNow,
                },
                new CreateUserCommandResponse(){ Succeeded = false }
            };
            yield return new object[]
            {
                new CreateUserDto
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Email= "wrongemaildotcom",
                    UserName = "TestMate",
                    Password = "strongpass1",
                    Date = DateTime.UtcNow,
                },
                new CreateUserCommandResponse(){ Succeeded = false }
            };
            yield return new object[]
            {
                new CreateUserDto
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Email= "testemail@gmail.com",
                    UserName = "TestMate",
                    Password = "123",
                    Date = DateTime.UtcNow,
                },
                new CreateUserCommandResponse(){ Succeeded = false }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
