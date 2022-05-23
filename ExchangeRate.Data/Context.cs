using ExchangeRate.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRate.Data
{
    public class Context : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Source> Sites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        { }
    }
}
