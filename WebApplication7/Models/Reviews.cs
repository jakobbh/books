namespace WebApplication7.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int RatingsSum { get; set; }
        public int ReviewsCount { get; set; }

        public Reviews()
        {
            RatingsSum = 0;
            ReviewsCount = 0;
        }
    }
}
