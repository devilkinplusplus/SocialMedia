using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Unit.Test.TestParameters.Users
{
    public class AssignRoleParameters : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                "some_user_id","User",true
            };
            yield return new object[]
            {
                "some_user_id","Admin",true
            };
            yield return new object[]
            {
                "some_user_id","Moderator",true
            };
            yield return new object[]
            {
                "some_user_id","Other",false
            };
            yield return new object[]
            {
                "some_user_id",null,false
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
