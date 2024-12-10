using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Domain.Entities.Base;
using SocialMediaBackend.Domain.Entities.Identity;

namespace SocialMediaBackend.Persistence.Contexts
{
    public class SocialMediaDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public SocialMediaDbContext(DbContextOptions options) : base(options)
        {
  
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<MessageThread> MessageThreads { get; set; }
        public DbSet<Message> Messages { get; set; }


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

    }
}
