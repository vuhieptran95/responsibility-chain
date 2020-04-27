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
        public DbSet<UserPolicy> UserPolicies { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<PolicyScope> PolicyScopes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientScope>(e => { e.HasKey(c => new {c.ClientId, c.ScopeId}); });

            modelBuilder.Entity<UserPolicy>(e =>
            {
                e.HasKey(u => new {u.Username, u.PolicyId});
                e.HasOne(u => u.User).WithMany(us => us.UserPolicies)
                    .HasForeignKey(u => u.Username);
            });

            modelBuilder.Entity<PolicyScope>(e => { e.HasKey(p => new {p.PolicyId, p.ScopeId}); });

        }
    }
}