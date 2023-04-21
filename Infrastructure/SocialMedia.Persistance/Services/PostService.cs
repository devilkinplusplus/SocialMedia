using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.Abstractions.Storage;
using SocialMedia.Application.Consts;
using SocialMedia.Application.DTOs.Post;
using SocialMedia.Application.Features.Commands.Post.Archive;
using SocialMedia.Application.Features.Commands.Post.Create;
using SocialMedia.Application.Features.Commands.Post.Edit;
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
        private readonly IPostReadRepository _postReadRepo;
        private readonly IFileService _fileService;
        public PostService(IPostWriteRepository postWriteRepo, IFileService fileService, IPostReadRepository postReadRepo)
        {
            _postWriteRepo = postWriteRepo;
            _fileService = fileService;
            _postReadRepo = postReadRepo;
        }

        public async Task ToggleArchivePostAsync(string id)
        {
            Post post = await _postReadRepo.GetByIdAsync(id);
            post.IsDeleted = !post.IsDeleted;
            _postWriteRepo.Update(post);
            await _postWriteRepo.SaveAsync();
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


            if (IsPostValid(postEntity.Content, post.Files))
            {
                if (validationResults.IsValid)
                {
                    await _postWriteRepo.SaveAsync();

                    if (post.Files is not null)
                        await _fileService.WritePostImagesAsync(postEntity.Id, post.Files);

                    return new() { Succeeded = validationResults.IsValid };
                }
                return new()
                {
                    Succeeded = validationResults.IsValid,
                    Errors = validationResults.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            return new() { Succeeded = false, Errors = new List<string>() { Messages.EmptyPostMessage } };

        }

        public async Task<bool> DeletePostAsync(string id)
        {
            bool res = await _postWriteRepo.RemoveAsync(id);
            await _postWriteRepo.SaveAsync();
            return res;
        }

        public async Task DeletePostImageAsync(string id)
        {
            await _fileService.DeletePostImageAsync(id);
        }

        public async Task<EditPostCommandResponse> EditPostAsync(EditPostDto post)
        {
            Post currentPost = await _postReadRepo.GetByIdAsync(post.Id);

            if (IsPostValid(post.Content, post.Files))
            {
                if (post.Files is not null)
                {
                    List<PostImage> postImages = await _fileService.WritePostImagesAsync(currentPost.Id, post.Files);
                    currentPost.PostImages = postImages;
                }

                currentPost.Content = post.Content ?? currentPost.Content;

                ValidationResult validationResult = await ValidatePostAsync(currentPost);
                if (validationResult.IsValid)
                {
                    _postWriteRepo.Update(currentPost);
                    await _postWriteRepo.SaveAsync();
                    return new() { Succeeded = true };
                }
                return new()
                {
                    Succeeded = true,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            return new() { Succeeded = false, Errors = new List<string>() { Messages.EmptyPostMessage } };


        }

        private bool IsPostValid(string content, IFormFileCollection files) => !(content is null && files is null);

        private async Task<ValidationResult> ValidatePostAsync(Post post)
        {
            PostValidator validationRules = new();
            return await validationRules.ValidateAsync(post);
        }



    }
}
