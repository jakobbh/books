using WebApplication7.Models;

namespace WebApplication7.Data.Interfaces
{
    public interface IRatingsRepository
    {
        bool AddReview(Ratings review);
        bool Save();
    }
}
