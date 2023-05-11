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
        public void APIController_AddReview_ReturnsSuccess()
        {
            //Arrange
            //var model2 = A.Fake<ReviewsandRatingViewModel>();
            var model = new ReviewsandRatingViewModel
            {
                Title = "Title",
                BookRating = 5,
                Author = "Author"
            };
            
            //Act
            var result = _apiController.AddReview(model);

            //Assert
            A.CallTo(() => _ratingsRepository.AddReview("Title", 5, "Author")).MustHaveHappenedOnceExactly();
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}

