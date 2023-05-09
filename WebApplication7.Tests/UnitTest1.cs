using WebApplication7.Controllers;
using WebApplication7.Data.Interfaces;
using WebApplication7.Repository;
using FakeItEasy;

namespace WebApplication7.Tests
{
    public class UnitTest1
    {
        private readonly IReviewsRepository _reviewRepository;
        private readonly APIController _APIController;

        public UnitTest1()
        {
            //Dependencies
            _reviewRepository = A.Fake<IReviewsRepository>();

            //SUT
            _APIController = new APIController(_reviewRepository);
        }

        [Theory]
        [InlineData(2,2,4)]
        [InlineData(-2.2, 2, -0.2)]
        public void Add_AddTwoNumbers_TheTwoNumbersShouldBeAdded(double x, double y, double expected)
        {
            // Arrange

            //Act
            var actual = Calculator.Add(x, y);

            //Assert
            Assert.Equal(expected, actual, 2);
        }
        [Fact]
        public void Divide_DivideTwoNumbers()
        {
            //Arrange
            var expected = 10;

            //Act
            var answer = Calculator.Divide(100, 10);

            //Assert
            Assert.Equal(expected, answer);

        }

    }
}