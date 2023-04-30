using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using WebApplication7.Data;
using WebApplication7.Data.Interfaces;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public IActionResult Index()
        {
            var bookList = _bookRepository.GetList();
            return View(bookList);
        }
        //public IActionResult ViewModel(string parameter1, int parameter2)
        //{
        //    ViewModel parameters = new ViewModel { _parameter1 = parameter1, _parameter2 = parameter2 };
        //    return View(parameters);
        //}
        [Authorize(Policy = "MustBeAuthor")]
        public IActionResult Details(int id)
        {
            Book book = _bookRepository.GetById(id);
            return View(book);
        }
        public IActionResult AddForm(Book book)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Book book)
        {
            _bookRepository.Add(book);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _bookRepository.Delete(id);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult TopList()
        {
            var topList = _bookRepository.GetTopList();
            return View(topList);
        }
        [HttpPost]
        public IActionResult Country(string country)
        {
            var countryList = _bookRepository.GetByCountry(country);
            return View(countryList);
        }
    }
}
