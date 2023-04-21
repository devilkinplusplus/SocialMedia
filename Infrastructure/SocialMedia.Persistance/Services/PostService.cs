using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.Abstractions.Storage;
using SocialMedia.Application.Consts;
using SocialMedia.Application.DTOs.Post;
using SocialMedia.Application.Features.Commands.Post.Create;
using SocialMedia.Application.Repositories.PostImages;
using SocialMedia.Application.Repositories.Posts;
using SocialMedia.Application.Validators;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Entities.Identity;
using SocialMedia.Persistance.Repositories.PostImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Persistance.Services
{
    public class PostService : IPostService
    {
        private readonly IPostWriteRepository _postWriteRepo;
        private readonly IFileService _fileService;
        public PostService(IPostWriteRepository postWriteRepo, IFileService fileService)
        {
            _postWriteRepo = postWriteRepo;
            _fileService = fileService;
        }

        public async Task<PostCreateCommandResponse> CreatePostAsync(CreatePostDto post)
        {
            Post postEntity = await _postWriteRepo.AddEntityAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                Content = post.Content,
                UserId = post.UserId,
            });

            ValidationResult validationResults = await ValidatePostAsync(postEntity);


            if (IsPostValid(postEntity, post.Files))
            {
                if (validationResults.IsValid)
                {
                    await _postWriteRepo.SaveAsync();

                    if (post.Files is not null)
                        await _fileService.WritePostImagesAsync(postEntity.Id, post.Files);

                    return new() { Succeeded = validationResults.IsValid };
                }
                return new() { Succeeded = validationResults.IsValid,
                               Errors = validationResults.Errors.Select(x => x.ErrorMessage).ToList() };
            }

            return new() { Succeeded = false, Errors = new List<string>() { Messages.EmptyPostMessage } };

        }

        private bool IsPostValid(Post post, IFormFileCollection files) => !(post.Content is null && files is null);

        private async Task<ValidationResult> ValidatePostAsync(Post post)
        {
            PostValidator validationRules = new();
            return await validationRules.ValidateAsync(post);
        }

    }
}
