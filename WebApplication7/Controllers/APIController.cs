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
        private readonly IBookRepository _bookRepository;
        private readonly IRatingsRepository _ratingsRepository;

        public APIController(IBookRepository bookRepository, IRatingsRepository ratingsRepository)
        {
            _bookRepository = bookRepository;
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

            var book = _bookRepository.GetByName(name);
            if (book != null)
            {
                ViewBag.book = book;
            } else
            {
                ViewBag.book = "This book is not reviewed yet. Do you want to add a review?";
            }

            ViewBag.title = title;
            ViewBag.author = author;
            ViewBag.description = description;
            return View();
        }
        public IActionResult MakeReview()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddReview(Ratings review)
        {
            _ratingsRepository.AddReview(review);
            return RedirectToAction("Index", "Home");

        }
    }
}
