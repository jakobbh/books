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

        public HomeController(ILogger<HomeController> logger, IReviewsRepository ratingsRepository)
        {
            _ratingsRepository = ratingsRepository;
            _logger = logger;
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
            _ratingsRepository.GetTopList();
            return View();
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