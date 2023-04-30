using WebApplication7.Models;

namespace WebApplication7.Data.Interfaces
{
    public interface IBookRepository
    {
        Book GetById(int id);
        IEnumerable<Book> GetList();
        bool Add(Book book);
        bool Delete(int id);
        IEnumerable<Book> GetTopList();
        IEnumerable<Book> GetByCountry(string country);
    }
}
