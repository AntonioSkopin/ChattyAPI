using ChattyAPI.Authorization.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChattyAPI.Authorization.Contexts
{
    public class AuthorizationDbContext : IdentityDbContext<ChattyUser>
    {
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}