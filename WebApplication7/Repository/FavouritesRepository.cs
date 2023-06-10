using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Data.Interfaces;
using WebApplication7.Models;

namespace WebApplication7.Repository
{
    public class FavouritesRepository : IFavouritesRepository
    {
        private readonly ApplicationDbContext _context;
        public FavouritesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FavouriteBooks> GetFavourites(string userId)
        {
            var favouriteList = _context.FavouriteBooks.FromSqlRaw($"SELECT * FROM FavouriteBooks WHERE userId = '{userId}'").ToList();
            return favouriteList;
        }
        public bool RemoveFavourite(string TitleAuthor)
        {
            var book = _context.FavouriteBooks.FirstOrDefault(i => i.TitleAuthor == TitleAuthor);
            _context.FavouriteBooks.Remove(book);
            return Save();
        }
        public FavouriteBooks GetFavourite(string TitleAuthor)
        {
            var favouriteBook = _context.FavouriteBooks.FirstOrDefault(i =>i.TitleAuthor == TitleAuthor);
            return favouriteBook;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
