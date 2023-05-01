using Microsoft.AspNetCore.Http;
using SocialMedia.Application.DTOs.Post;
using SocialMedia.Application.Features.Commands.Post.Edit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Unit.Test.TestParameters.Posts
{
    public class EditPostParameters : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new EditPostDto()
                {
                    Id = "some_user_id",
                    Content = "Some content",
                    Files = new FormFileCollection { new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file")), 0, 0, "test", "test.txt") },
                },
                new EditPostCommandResponse(){Succeeded = true}
            };
            yield return new object[]
            {
                new EditPostDto()
                {
                    Id = "some_user_id",
                    Content = null,
                    Files = new FormFileCollection { new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file")), 0, 0, "test", "test.txt") },
                },
                new EditPostCommandResponse(){Succeeded = true}

            };
            yield return new object[]
            {
                new EditPostDto()
                {
                    Id = "some_user_id",
                    Content = "Some content",
                    Files = null,
                },
                new EditPostCommandResponse(){Succeeded = true}
            };
            yield return new object[]
            {
                new EditPostDto()
                {
                    Id = "some_user_id",
                    Content = null,
                    Files = null,
                },
                new EditPostCommandResponse(){Succeeded = false}
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
