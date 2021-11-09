using MentorsBlog.Core.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace MentorsBlog.Core.DataAccess
{
    public class IdentityDbContext : DbContext
    {
        public DbSet<DbPost> Posts { get; set; }
        public DbSet<DbComment> Comments { get; set; }
        public DbSet<DbUser> Users { get; set; }

        public IdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}