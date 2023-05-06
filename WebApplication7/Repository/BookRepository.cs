using Microsoft.EntityFrameworkCore.Diagnostics;
using WebApplication7.Data;
using WebApplication7.Data.Interfaces;
using WebApplication7.Models;

namespace WebApplication7.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Book GetByName(string name)
        {
            var book = _context.Books7.FirstOrDefault<Book>(i => i.Title == name);
            return book;
        }
        public bool AddBook(Book book)
        {
            _context.Books7.Add(book);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
