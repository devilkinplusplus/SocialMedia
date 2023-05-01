using SocialMedia.Application.DTOs.User;
using SocialMedia.Application.Features.Commands.User.Edit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Unit.Test.TestParameters.Users
{
    public class EditUserParameters : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new EditUserDto
                {
                    Id = "some_user_id",
                    FirstName = "Test",   
                    LastName = "Test",
                    About = "Test",
                    UserName = "NewTestName",
                    Date = DateTime.UtcNow,
                },
                new EditUserCommandResponse(){ Succeeded = true },
            };
            yield return new object[]
            {
                new EditUserDto
                {
                    Id = "some_user_id",
                    FirstName = null,
                    LastName = null,
                    About = "Test",
                    UserName = "NewTestName",
                    Date = DateTime.UtcNow,
                },
                new EditUserCommandResponse(){ Succeeded = false },
            };
            yield return new object[]
            {
                new EditUserDto
                {
                    Id = "some_user_id",
                    FirstName = "Test",
                    LastName = "Test",
                    About = null,
                    UserName = "NewTestName",
                    Date = DateTime.UtcNow,
                },
                new EditUserCommandResponse(){ Succeeded = true },
            };
            yield return new object[]
            {
                new EditUserDto
                {
                    Id = "some_user_id",
                    FirstName = "Test",
                    LastName = "Test",
                    About = "test",
                    UserName = "",
                    Date = DateTime.UtcNow,
                },
                new EditUserCommandResponse(){ Succeeded = false },
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
