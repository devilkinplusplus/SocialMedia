using AutoMapper;
using FluentValidation.Results;
using SocialMedia.Application.Abstractions.Services;
using SocialMedia.Application.DTOs.Comment;
using SocialMedia.Application.DTOs.User;
using SocialMedia.Application.Features.Commands.Comment.Create;
using SocialMedia.Application.Repositories.Comments;
using SocialMedia.Application.Validators;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Entities.Identity;

namespace SocialMedia.Persistance.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentWriteRepository _commentWriteRepo;
        private readonly ICommentReadRepository _commentReadRepo;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public CommentService(ICommentWriteRepository commentWriteRepo, ICommentReadRepository commentReadRepo, IUserService userService, IMapper mapper)
        {
            _commentWriteRepo = commentWriteRepo;
            _commentReadRepo = commentReadRepo;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<CreateCommentCommandResponse> CreateCommentAsync(CreateCommentDto comment)
        {
            Comment commentEntity = await _commentWriteRepo.AddEntityAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                PostId = comment.PostId,
                UserId = comment.UserId,
                Content = comment.Content
            });

            ValidationResult result = await ValidateCommentAsync(commentEntity);

            if (result.IsValid)
            {
                await _commentWriteRepo.SaveAsync();
                UserListDto userDto = _userService.GetUserById(comment.UserId);
                var mapper = _mapper.Map<User>(userDto);
                commentEntity.User = mapper;
                return new() { Succeeded = true, Comment = commentEntity };
            }

            return new() { Succeeded = false, Errors = result.Errors.Select(x => x.ErrorMessage).ToList() };
        }

        public async Task DeleteCommentAsync(string commentId)
        {
            await _commentWriteRepo.RemoveAsync(commentId);
            await _commentWriteRepo.SaveAsync();
        }

        public async Task SoftDeleteCommentAsync(string commentId)
        {
            Comment comment = await _commentReadRepo.GetByIdAsync(commentId);
            comment.IsDeleted = true;
            await _commentWriteRepo.SaveAsync();
        }

        private async Task<ValidationResult> ValidateCommentAsync(Comment comment)
        {
            CommentValidator validationRules = new();
            return await validationRules.ValidateAsync(comment);
        }

    }
}
