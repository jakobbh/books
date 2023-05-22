using WebApplication7.Models;

namespace WebApplication7.Data.Interfaces
{
    public interface IUserRepository
    {
        public ApplicationUser GetUser(string email);
    }
}
