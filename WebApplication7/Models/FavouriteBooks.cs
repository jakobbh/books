using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class FavouriteBooks
    {
        public string UserId { get; set; }
        public string TitleAuthor { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
    }
}
