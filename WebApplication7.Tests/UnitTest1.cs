using WebApplication7.Repository;

namespace WebApplication7.Tests
{
    public class UnitTest1
    {
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
            var expected = 0;

            //Act
            var answer = Calculator.Divide(100, 0);

            //Assert
            Assert.Equal(expected, answer);

        }
    }
}