namespace WebApplication7.Models
{
    public class ReviewsandRatingViewModel
    {
        public Reviews reviews { get; set; }
        public int BookRating { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int RatingSum { get; set; }
        public int RatingCount { get; set; }
        public string TitleAuthor { get; set; }
    }
}
