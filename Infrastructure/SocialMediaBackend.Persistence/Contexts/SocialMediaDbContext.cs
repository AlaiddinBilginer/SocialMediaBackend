using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Domain.Entities.Base;
using SocialMediaBackend.Domain.Entities.Identity;
using System.Reflection.Emit;

namespace SocialMediaBackend.Persistence.Contexts
{
    public class SocialMediaDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public SocialMediaDbContext(DbContextOptions options) : base(options)
        {
  
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<Entity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow,
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PostComment>()
                .HasOne(pc => pc.ParentComment)
                .WithMany(pc => pc.Replies)  
                .HasForeignKey(pc => pc.ParentCommentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Follower>()
                .HasIndex(f => new { f.FollowerUserId, f.FollowedUserId })
                .IsUnique();

            builder.Entity<Follower>()
                .HasOne(f => f.FollowerUser)
                .WithMany(u => u.Following)
                .HasForeignKey(f => f.FollowerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Follower>()
                .HasOne(f => f.FollowedUser)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.FollowedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
