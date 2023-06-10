using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication7.Data.Interfaces;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReviewsRepository _ratingsRepository;
        private readonly IFavouritesRepository _favouritesRepository;
        private readonly IUserRepository _userRepository;

        public HomeController(IReviewsRepository ratingsRepository, IFavouritesRepository favouritesRepository, IUserRepository userRepository)
        {
            _ratingsRepository = ratingsRepository;
            _favouritesRepository = favouritesRepository;
            _userRepository = userRepository;
            //_logger = logger;
        }
        public IActionResult Index()
        {
            var HomePageViewModel = new HomePageViewModel();
            HomePageViewModel.books = _ratingsRepository.GetTopList();
            HomePageViewModel.loginModel = new LoginModel();
            return View(HomePageViewModel);
        }
        public IActionResult Details(string name)
        {
            var book = _ratingsRepository.GetByTitle(name);
            return View(book);
        }

        public IActionResult Favourite(string title, string author, string username)
        {
            var TitleAuthor = title + author;
            if(_favouritesRepository.GetFavourite(TitleAuthor) != null)
            {
                return RedirectToAction("Index");
            }
            _ratingsRepository.Favourite(title, author, username);
            return RedirectToAction("Index");
        }

        public IActionResult MyFavourites()
        {
            var username = User.Identity.Name;
            var user = _userRepository.GetUserByUsername(username);
            var userId = user.Id;
            var favouritesList = _favouritesRepository.GetFavourites(userId);
            return View(favouritesList);
        }
        [HttpPost]
        public IActionResult RemoveFavourite(string TitleAuthor)
        {
            _favouritesRepository.RemoveFavourite(TitleAuthor);
            return RedirectToAction("MyFavourites");
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}