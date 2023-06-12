using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text.Json;
using WebApplication7.Data.Interfaces;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class APIController : Controller
    {
        private readonly IReviewsRepository _ratingsRepository;

        public APIController(IReviewsRepository ratingsRepository)
        {
            _ratingsRepository = ratingsRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindBook(string author, string title)
        {

            var client = new HttpClient();
            var endpoint = new Uri($"https://www.googleapis.com/books/v1/volumes?q={title}+inauthor:{author}");

            var result = client.GetAsync(endpoint).Result.Content.ReadAsStringAsync().Result;
            var deserializedData = JsonSerializer.Deserialize<JsonElement>(result);

            var items = deserializedData.GetProperty("items").EnumerateArray();
            HttpContext.Session.SetString("book_items", deserializedData.ToString());
            var books = new List<List<string>>();
            foreach (var item in items )
            {
                string jsontitle = item.GetProperty("volumeInfo").GetProperty("title").GetString();
                string jsonauthor = item.GetProperty("volumeInfo").GetProperty("authors").EnumerateArray().FirstOrDefault().GetString();
                List<string> bookInfo = new List<string> { jsontitle, jsonauthor };
                books.Add(bookInfo);
            }
            var distinct_titles = books.Distinct(new ListEqualityComparer()).ToList();
            ViewBag.title = distinct_titles;
            return View();
        }
        public IActionResult BookDetails(int id, string name)
        {
            var raw_items = JsonSerializer.Deserialize<JsonElement>(HttpContext.Session.GetString("book_items"));
            var items = raw_items.GetProperty("items").EnumerateArray().ToArray();
            string title = items[id].GetProperty("volumeInfo").GetProperty("title").GetString();
            string author = items[id].GetProperty("volumeInfo").GetProperty("authors")[0].GetString();
            string description = "test";

            var book = _ratingsRepository.GetByTitle(name);
            var rating = (double)book.RatingSum / book.RatingCount;
            if (book == null)
            {
                var newModel = new ReviewsandRatingViewModel
                {
                    Title = title,
                    Author = author,
                    RatingCount = 0,
                    RatingSum = 0,
                    Rating = 0
                };
                return View(newModel);
            }
            var model = new ReviewsandRatingViewModel
            {
                Title = book.Title,
                Author = book.Author,
                RatingCount = book.RatingCount,
                RatingSum = book.RatingSum,
                Rating = rating
            };
            return View(model);
        }
        public IActionResult MakeReview()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddReview(ReviewsandRatingViewModel model)
        {
            _ratingsRepository.AddReview(model.Title, model.BookRating, model.Author);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(string title, string author)
        {
            _ratingsRepository.Delete(title, author);
            return RedirectToAction("Index", "Home");
        }
    }
}
public class ListEqualityComparer : IEqualityComparer<List<string>>
{
    public bool Equals(List<string> x, List<string> y)
    {
        if (ReferenceEquals(x, y))
            return true;
        if (x is null || y is null)
            return false;
        return x.SequenceEqual(y);
    }

    public int GetHashCode(List<string> obj)
    {
        if (obj is null)
            return 0;
        unchecked
        {
            int hash = 19;
            foreach (string item in obj)
            {
                hash = hash * 31 + item?.GetHashCode() ?? 0;
            }
            return hash;
        }
    }
}