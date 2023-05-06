using WebApplication7.Models;

namespace WebApplication7.Data.Interfaces
{
    public interface IBookReviewRepository
    {
        BookReview GetById(int id);
        IEnumerable<BookReview> GetList();
        bool Add(BookReview book);
        bool Delete(int id);
        IEnumerable<BookReview> GetTopList();
        IEnumerable<BookReview> GetByCountry(string country);
    }
}
