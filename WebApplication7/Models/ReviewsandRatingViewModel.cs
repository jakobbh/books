using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class ReviewsandRatingViewModel
    {
        public Reviews reviews { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int BookRating { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageLink { get; set; }
        public int RatingSum { get; set; }
        public int RatingCount { get; set; }
        public string TitleAuthor { get; set; }
        public double Rating => RatingCount > 0 ? (double)RatingSum / RatingCount : 0;

    }

}
