using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Controllers;
using WebApplication7.Data.Interfaces;
using WebApplication7.Models;

namespace WebApplication7.Tests.Controller
{
    public class APIControllerTests
    {
        private IReviewsRepository _ratingsRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private APIController _apiController;
        public APIControllerTests()
        {
            //Dependencies
            _ratingsRepository = A.Fake<IReviewsRepository>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();

            //SUT
            _apiController = new APIController(_ratingsRepository);
        }
        [Fact]
        public void APIController_Detail_ReturnsSuccess()
        {
            //Arrange
            var id = 1;
            var name = "Name";
            var book = A.Fake<Reviews>();
            A.CallTo(() => _ratingsRepository.GetByTitle(name));
            //Act
            var result = _apiController.BookDetails(id, name);

            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
