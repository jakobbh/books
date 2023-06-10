using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Reviews> Reviews2 { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<FavouriteBooks> FavouriteBooks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavouriteBooks>()
                .HasKey(fb => new { fb.UserId, fb.TitleAuthor });
        }
    }
}
