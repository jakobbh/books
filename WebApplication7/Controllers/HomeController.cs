﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication7.Data.Interfaces;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReviewsRepository _ratingsRepository;

        public HomeController(IReviewsRepository ratingsRepository)
        {
            _ratingsRepository = ratingsRepository;
            //_logger = logger;
        }
        public IActionResult Index()
        {
            var HomePageViewModel = new HomePageViewModel();
            HomePageViewModel.books = _ratingsRepository.GetList();
            HomePageViewModel.loginModel = new LoginModel();
            return View(HomePageViewModel);
        }
        public IActionResult TopList()
        {
            var topList = _ratingsRepository.GetTopList();
            return View(topList);
        }
        public IActionResult Details(string name)
        {
            var book = _ratingsRepository.GetByTitle(name);
            return View(book);
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