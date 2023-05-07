using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class Reviews
    {
        [Key]
        public string TitleAuthor { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int RatingSum { get; set; }
        public int RatingCount { get; set; }
        public int Rating { get; set; }

        public Reviews()
        {
            RatingSum = 0;
            RatingCount = 0;
            Rating = 0;
        }
    }
}
