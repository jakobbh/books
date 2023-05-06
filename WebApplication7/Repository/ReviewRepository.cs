using Microsoft.AspNetCore.Mvc;
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
            return _context.Reviews.ToList();
        }
        public Reviews GetByTitle(string title)
        {
            var review = _context.Reviews.FirstOrDefault(i => i.Title == title);
            return review;
        }
        public bool AddReview(string title, int bookrating, int id)
        {
            if (_context.Reviews.FirstOrDefault<Reviews>(i => i.Title == title) == null)
            {
                Reviews review = new Reviews();
                review.Id = id;
                review.Title = title;
                review.ReviewsCount = 1;
                review.RatingsSum = bookrating;
                _context.Reviews.Add(review);
            }
            else
            {
                var bookReview = _context.Reviews.FirstOrDefault<Reviews>(i => i.Title == title);
                bookReview.RatingsSum = bookReview.RatingsSum + bookrating;
                bookReview.ReviewsCount = bookReview.ReviewsCount + 1;
            }
            return Save();
        }
        public bool Delete(int id)
        {
            var book = _context.Reviews.FirstOrDefault(i => i.Id == id);
            _context.Remove(book);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
