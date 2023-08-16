﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Persistance.Contexts
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<BaseFile> BaseFiles { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<PostReaction> PostReactions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<UserRank> UserRanks { get; set; }

        //Interceptor
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var data = ChangeTracker.Entries<BaseEntity>();

            foreach (var item in data)
            {
                item.Entity.Date = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Follow>()
                .HasOne(x => x.Follower)
                .WithMany(x => x.Followers)
                .HasForeignKey(x => x.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Follow>()
                .HasOne(x => x.Following)
                .WithMany(x => x.Followings)
                .HasForeignKey(x => x.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserRank>().HasKey(x => new { x.RankId, x.UserId });

            builder.Entity<UserRank>()
                    .HasOne(x => x.User)
                    .WithMany(x => x.UserRanks)
                    .HasForeignKey(x => x.UserId);

            builder.Entity<UserRank>()
                    .HasOne(x => x.Rank)
                    .WithMany(x => x.UserRanks)
                    .HasForeignKey(x => x.RankId);

            base.OnModelCreating(builder);
        }

    }
}
