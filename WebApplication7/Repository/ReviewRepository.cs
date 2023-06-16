using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Data.Interfaces;
using WebApplication7.Models;

namespace WebApplication7.Repository
{
    public class ReviewRepository : IReviewsRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Reviews> GetList()
        {
            return _context.Reviews2.ToList();
        }
        public Reviews GetByTitle(string name)
        {
            var review = _context.Reviews2.FirstOrDefault(i => i.TitleAuthor == name);
            return review;
        }
        public IEnumerable<Reviews> GetTopList()
        {
            var topList = _context.Reviews2.FromSqlRaw($"SELECT TOP 3 * FROM Reviews2 ORDER BY ratingSum DESC").ToList();
            return topList;
        }
        public bool AddReview(string title, int bookrating, string author, string imageLink)
        {
            if (_context.Reviews2.FirstOrDefault<Reviews>(i => i.Title == title) == null)
            {
                Reviews review = new Reviews();
                review.TitleAuthor = title + author;
                review.Author = author;
                review.Title = title;
                review.RatingCount = 1;
                review.RatingSum = bookrating;
                review.Rating = bookrating;
                review.ImageLink = imageLink;
                _context.Reviews2.Add(review);
            }
            else
            {
                var bookReview = _context.Reviews2.FirstOrDefault<Reviews>(i => i.Title == title);
                var rating = bookReview.RatingSum + bookrating / bookReview.RatingCount + 1;
                bookReview.RatingSum = bookReview.RatingSum + bookrating;
                bookReview.RatingCount = bookReview.RatingCount + 1;
                bookReview.Rating = rating;
            }
            return Save();
        }
        public bool Delete(string titleAuthor)
        {
            var book = _context.Reviews2.FirstOrDefault(i => i.TitleAuthor == titleAuthor);
            _context.Remove(book);
            return Save();
        }
        public bool Favourite(string title, string author, string username)
        {
            var book = _context.Reviews2.FirstOrDefault(i => i.TitleAuthor == title+author);
            var user = _context.AspNetUsers.FirstOrDefault(u => u.UserName == username);
            var favourite = new FavouriteBooks
            {
                Author = author,
                Title = title,
                TitleAuthor = title + author,
                UserId = user.Id
            };
            _context.FavouriteBooks.Add(favourite);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
