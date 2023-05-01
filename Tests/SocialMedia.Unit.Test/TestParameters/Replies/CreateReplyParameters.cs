using SocialMedia.Application.DTOs.Reply;
using SocialMedia.Application.Features.Commands.Reply.Create;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Unit.Test.TestParameters.Replies
{
    public class CreateReplyParameters : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new CreateReplyDto
                {
                    UserId = "some_user_id",
                    CommentId = "some_comment_id",
                    Content = "Some content"
                },
                new CreateReplyCommandResponse(){Succeeded = true}
            };
            yield return new object[]
            {
                new CreateReplyDto
                {
                    UserId = "some_user_id",
                    CommentId = "some_comment_id",
                    Content = null
                },
                new CreateReplyCommandResponse(){Succeeded = false}
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
