using Microsoft.AspNetCore.Http;
using SocialMedia.Application.DTOs.Post;
using SocialMedia.Application.Features.Commands.Post.Create;
using SocialMedia.Domain.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Unit.Test.TestParameters.Posts
{
    public class CreatePostParameters : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new CreatePostDto
                {
                    Content = "Some Content",
                    Files = new FormFileCollection { new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file")), 0, 0, "test", "test.txt") },
                    UserId = "54acf07c-f362-4350-be93-bed7b5e40ee4"
                },
                new PostCreateCommandResponse { Succeeded = true }
            };
            yield return new object[]
            {
                new CreatePostDto
                {
                    Content = "Some Content",
                    Files = null,
                    UserId = "54acf07c-f362-4350-be93-bed7b5e40ee4"
                },
                new PostCreateCommandResponse { Succeeded = true }
            };
            yield return new object[]
            {
                new CreatePostDto
                {
                    Content = null,
                    Files = new FormFileCollection { new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file")), 0, 0, "test", "test.txt") },
                    UserId = "54acf07c-f362-4350-be93-bed7b5e40ee4"
                },
                new PostCreateCommandResponse { Succeeded = true }
            };
            yield return new object[]
            {
                new CreatePostDto
                {
                    Content = null,
                    Files = new FormFileCollection { new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file")), 0, 0, "test", "test.txt") },
                    UserId = null
                },
                new PostCreateCommandResponse { Succeeded = false }
            };
            yield return new object[]
           {
                new CreatePostDto
                {
                    Content = null,
                    Files = null,
                    UserId = "54acf07c-f362-4350-be93-bed7b5e40ee4"
                },
                new PostCreateCommandResponse { Succeeded = false }
           };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
