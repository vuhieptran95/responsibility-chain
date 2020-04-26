using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Features.Domains
{
    public class IdPDbContext : DbContext
    {
        public IdPDbContext(DbContextOptions<IdPDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientScope> ClientScopes { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<ScopeProvider> ScopeProviders { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserScope> UserScopes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientScope>(e => { e.HasKey(c => new {c.ClientId, c.ScopeId}); });
            modelBuilder.Entity<UserScope>(e =>
            {
                e.HasKey(c => new {c.Username, c.ScopeId});
                e.HasOne(e => e.User).WithMany(u => u.UserScopes)
                    .HasForeignKey(e => e.Username);
            });
        }
    }
}