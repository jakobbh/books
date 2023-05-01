using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text.Json;

namespace WebApplication7.Controllers
{
    public class APIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindBook(string author, string title)
        {
            string _author = author;
            string _title = title;

            var client = new HttpClient();
            var endpoint = new Uri(
                $"https://www.googleapis.com/books/v1/volumes?q={_title}+inauthor:{author}");

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
        public IActionResult BookDetails(int id)
        {
            var raw_items = JsonSerializer.Deserialize<JsonElement>(HttpContext.Session.GetString("book_items"));
            var items = raw_items.GetProperty("items").EnumerateArray().ToArray();
            string title = items[id].GetProperty("volumeInfo").GetProperty("title").GetString();
            string author = items[id].GetProperty("volumeInfo").GetProperty("authors")[0].GetString();
            string description = "test";

            ViewBag.title = title;
            ViewBag.author = author;
            ViewBag.description = description;
            return View();
        }
    }
}
