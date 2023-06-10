using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication7.Controllers;
using WebApplication7.Data.Interfaces;
using WebApplication7.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Http;

namespace WebApplication7.Tests.Controller
{
    public class HomeControllerTests
    {

        private readonly IReviewsRepository _ratingsRepository;
        private readonly IFavouritesRepository _favouritesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private HomeController _homeController;
        public HomeControllerTests()
        {
            //Dependencies
            _ratingsRepository = A.Fake<IReviewsRepository>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();

            //SUT
            _homeController = new HomeController(_ratingsRepository, _favouritesRepository, _userRepository);
        } 
        [Fact]
        public void HomeController_Index_ReturnsSuccess()
        {
            //Arrange
            var books = A.Fake<IEnumerable<Reviews>>();
            A.CallTo(() => _ratingsRepository.GetList()).Returns(books);

            //Act
            var result = _homeController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
