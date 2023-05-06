using WebApplication7.Models;

namespace WebApplication7.Data.Interfaces
{
    public interface IBookRepository
    {
        Book GetByName(string name);
        bool AddBook(Book book);
    }
}
