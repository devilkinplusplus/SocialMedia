using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Abstractions.Caching;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.Consts;
using SocialMedia.Application.DTOs.Post;
using SocialMedia.Application.Features.Commands.Post.Create;
using SocialMedia.Application.Features.Commands.Post.Edit;
using SocialMedia.Application.Features.Queries.Post.GetAll;
using SocialMedia.Application.Features.Queries.Post.GetMyPosts;
using SocialMedia.Application.Repositories.Posts;
using SocialMedia.Application.Validators;
using SocialMedia.Domain.Entities;
using SocialMedia.Application.Abstractions.Storage;
using AutoMapper;
using SocialMedia.Application.Features.Queries.Post.Get;
using SocialMedia.Domain.Entities.Identity;
using System.Drawing;

namespace SocialMedia.Persistance.Services
{
    public class PostService : IPostService
    {
        private readonly IPostWriteRepository _postWriteRepo;
        private readonly IPostReadRepository _postReadRepo;
        private readonly IFileService _fileService;
        private readonly IPostReactionService _postReactionService;
        private readonly ICacheService _cacheService;
        private readonly IUserService _userService;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;
        public PostService(IPostWriteRepository postWriteRepo, IFileService fileService, IPostReadRepository postReadRepo, IPostReactionService postReactionService, ICacheService cacheService, IUserService userService, IStorageService storageService, IMapper mapper)
        {
            _postWriteRepo = postWriteRepo;
            _fileService = fileService;
            _postReadRepo = postReadRepo;
            _postReactionService = postReactionService;
            _cacheService = cacheService;
            _userService = userService;
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task ToggleArchivePostAsync(string id)
        {
            Post post = await _postReadRepo.GetByIdAsync(id);
            post.IsDeleted = !post.IsDeleted;
            _postWriteRepo.Update(post);
            await _postWriteRepo.SaveAsync();
        }

        public async Task<PostCreateCommandResponse> CreatePostAsync(string content, string userId, IFormFileCollection files)
        {
            if (userId is null)
                return new PostCreateCommandResponse() { Succeeded = false };

            Post postEntity = await _postWriteRepo.AddEntityAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                Content = content,
                UserId = userId,
            });

            ValidationResult validationResults = await ValidatePostAsync(postEntity);


            if (IsPostValid(postEntity.Content, files))
            {
                if (validationResults.IsValid)
                {
                    await _postWriteRepo.SaveAsync();

                    if (files is not null)
                        await _fileService.WritePostImagesAsync(postEntity.Id, files);

                    var mapper = _mapper.Map<PostListDto>(postEntity);
                    mapper.User = _userService.GetUserById(userId);
                    return new() { Succeeded = validationResults.IsValid, Post = mapper };
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

        public async Task<EditPostCommandResponse> EditPostAsync(string id, string content, IFormFileCollection files)
        {
            Post currentPost = await _postReadRepo.GetByIdAsync(id);

            if (IsPostValid(content, files))
            {
                if (files is not null)
                {
                    List<PostImage> postImages = await _fileService.WritePostImagesAsync(currentPost.Id, files);
                    currentPost.PostImages = postImages;
                }

                currentPost.Content = content ?? currentPost.Content;

                ValidationResult validationResult = await ValidatePostAsync(currentPost);
                if (validationResult.IsValid)
                {
                    _postWriteRepo.Update(currentPost);
                    await _postWriteRepo.SaveAsync();

                    var mappedPost = _mapper.Map<PostListDto>(currentPost);
                    return new() { Succeeded = true, Post = mappedPost };
                }
                return new()
                {
                    Succeeded = false,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            return new() { Succeeded = false, Errors = new List<string>() { Messages.EmptyPostMessage } };


        }

        private bool IsPostValid(string content, IFormFileCollection files) => !(content is null && files is null);

        public async Task<GetAllPostsQueryResponse> GetAllPostsAsync(string userId, int page = 0, int size = 5)
        {

            var posts = await _postReadRepo.GetAllWhere(x => x.IsDeleted == false).Include(z => z.User).Include(x => x.PostImages)
                .Include(x => x.Comments).ThenInclude(x => x.User).ThenInclude(x => x.ProfileImage)
                .Include(x => x.Comments).ThenInclude(x => x.Replies)
                .OrderByDescending(x => x.Date)
                .Select(x => new PostListDto()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Content = x.Content,
                    Files = x.PostImages.Select(x => x.Path),
                    Comments = x.Comments.Where(x => x.IsDeleted == false),
                    User = _userService.GetUserById(x.UserId),
                    Likes = _postReactionService.GetPostReactions(x.Id),
                    IsLiked = _postReactionService.IsAlreadyLiked(userId, x.Id),
                    Date = x.Date
                })
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            int count = await _postReadRepo.GetAllWhere(x => x.IsDeleted == false).CountAsync();

            if (!posts.Any())
                return new() { Succeeded = false, Errors = new List<string>() { Messages.NoPostsFoundMessage } };

            return new() { Succeeded = true, Values = posts, PostCount = count };
        }

        public async Task<GetMyPostsQueryResponse> GetMyPostsAsync(string userId,string authId, int page = 0, int size = 5)
        {

            var posts = await _postReadRepo.GetAllWhere(x => x.UserId == userId && x.IsDeleted == false).Include(z => z.User).Include(x => x.PostImages)
                .Include(x => x.Comments).ThenInclude(x => x.User).ThenInclude(x => x.ProfileImage)
                .Include(x => x.Comments).ThenInclude(x => x.Replies)
                .OrderByDescending(x => x.Date)
                .Select(x => new PostListDto()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Content = x.Content,
                    Files = x.PostImages.Select(x => x.Path),
                    Comments = x.Comments.Where(x => x.IsDeleted == false),
                    User = _userService.GetUserById(x.UserId),
                    Likes = _postReactionService.GetPostReactions(x.Id),
                    IsLiked = _postReactionService.IsAlreadyLiked(authId, x.Id),
                    Date = x.Date
                })
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            int count = await _postReadRepo.GetAllWhere(x => x.UserId == userId && x.IsDeleted == false).CountAsync();


            if (!posts.Any())
                return new() { Succeeded = false, Errors = new List<string>() { Messages.NoPostsFoundMessage } };

            return new() { Succeeded = true, Values = posts, PostCount = count };
        }

        private async Task<ValidationResult> ValidatePostAsync(Post post)
        {
            PostValidator validationRules = new();
            return await validationRules.ValidateAsync(post);
        }
        private async Task CachePostsAsync(IEnumerable<PostListDto> posts, string key)
        {
            //Store cache
            await _cacheService.SetAsync<IEnumerable<PostListDto>>(key, posts, TimeSpan.FromMinutes(30));
        }

        public async Task<GetPostCommandResponse> GetPostAsync(string postId)
        {
            Post post = await _postReadRepo.GetAsync(x => x.Id == postId, "PostImages");
            PostListDto postDto = _mapper.Map<PostListDto>(post);

            postDto.FileInfos = post.PostImages.Select(postImage => new FileDto
            {
                FileId = postImage.Id,
                Path = postImage.Path
            }).ToList();


            if (postDto is null)
                return new() { Succeeded = false, Errors = new List<string>() { Messages.NoPostsFoundMessage } };
            return new() { Succeeded = true, Value = postDto };
        }

    }
}
