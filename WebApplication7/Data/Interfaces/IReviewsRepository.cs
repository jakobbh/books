﻿using WebApplication7.Models;

namespace WebApplication7.Data.Interfaces
{
    public interface IReviewsRepository
    {
        Reviews GetByTitle(string name);
        IEnumerable<Reviews> GetTopList();
        bool AddReview(string title, int rating, string author, string imageLink);
        IEnumerable<Reviews> GetList();
        bool Delete(string titleAuthor);
        bool Favourite(string title, string author, string username);
        bool Save();
    }
}
