using WebApplication7.Models;

namespace WebApplication7.Data.Interfaces
{
    public interface IRatingsRepository
    {
        bool AddReview(string title, int rating, int id);
        IEnumerable<Reviews> GetList();
        bool Delete(int id);
        bool Save();
    }
}
