using WebApplication7.Models;

namespace WebApplication7.Data.Interfaces
{
    public interface IReviewsRepository
    {
        Reviews GetByTitle(string title);
        IEnumerable<Reviews> GetTopList();
        bool AddReview(string title, int rating, string author);
        IEnumerable<Reviews> GetList();
        bool Delete(string title, string author);
        bool Save();
    }
}
