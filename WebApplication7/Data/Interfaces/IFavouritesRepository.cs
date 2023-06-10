using WebApplication7.Models;

namespace WebApplication7.Data.Interfaces
{
    public interface IFavouritesRepository
    {
        IEnumerable<FavouriteBooks> GetFavourites(string userId);
        bool RemoveFavourite(string TitleAuthor);
        FavouriteBooks GetFavourite(string TitleAuthor);
    }
}
