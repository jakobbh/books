using WebApplication7.Data;
using WebApplication7.Data.Interfaces;
using WebApplication7.Models;

namespace WebApplication7.Repository
{
    public class RatingsRepository : IRatingsRepository
    {
        private readonly ApplicationDbContext _context;
        public RatingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddReview(Ratings review)
        {
            _context.Ratings.Add(review);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
