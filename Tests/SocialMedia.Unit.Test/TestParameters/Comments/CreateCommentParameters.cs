using SocialMedia.Application.DTOs.Comment;
using SocialMedia.Application.Features.Commands.Comment.Create;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Unit.Test.TestParameters.Comments
{
    public class CreateCommentParameters : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new CreateCommentDto
                {
                    PostId ="some_post_id",
                    UserId = "some_user_id",
                    Content = "Some content"
                },
                new CreateCommentCommandResponse(){ Succeeded = true }
            };
            yield return new object[]
            {
                new CreateCommentDto
                {
                    PostId ="some_post_id",
                    UserId = "some_user_id",
                    Content = null
                },
                new CreateCommentCommandResponse(){ Succeeded = false }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
