using SocialMedia.Application.Features.Commands.User.ChangePassword;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Unit.Test.TestParameters.Users
{
    public class ChangePasswordParameters : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                "some_user_id","newPassword",new ChangePasswordCommandResponse(){ Succeeded = true },
            };
            yield return new object[]
            {
                "some_user_id","somepassword",new ChangePasswordCommandResponse(){ Succeeded = true },
            };
            yield return new object[]
            {
                "some_user_id","123456789",new ChangePasswordCommandResponse(){ Succeeded = true },
            };
            yield return new object[]
            {
                "some_user_id","SOMEPASSWORD",new ChangePasswordCommandResponse(){ Succeeded = true },
            };
            yield return new object[]
            {
                "some_user_id","12345",new ChangePasswordCommandResponse(){ Succeeded = false },
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
