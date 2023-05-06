using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<BookReview> Books5 { get; set; }
        public DbSet<Book> Books7 { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
    }
}
