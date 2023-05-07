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
            var endpoint = new Uri(
                $"https://www.googleapis.com/books/v1/volumes?q={title}+inauthor:{author}");

            var result = client.GetAsync(endpoint).Result.Content.ReadAsStringAsync().Result;
            var deserializedData = JsonSerializer.Deserialize<JsonElement>(result);

            var items = deserializedData.GetProperty("items").EnumerateArray();
            HttpContext.Session.SetString("book_items", deserializedData.ToString());
            var titles = new List<string>();
            foreach (var item in items )
            {
                string jsontitle = item.GetProperty("volumeInfo").GetProperty("title").GetString();
                titles.Add(jsontitle);
            }

            ViewBag.title = titles;
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
            if (book == null)
            {
                var newModel = new ReviewsandRatingViewModel
                {
                    Title = title,
                    Author = author,
                    RatingCount = 0,
                    RatingSum = 0
                };
                return View(newModel);
            }
            var model = new ReviewsandRatingViewModel
            {
                reviews = book
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
