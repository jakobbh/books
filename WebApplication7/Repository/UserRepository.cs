using WebApplication7.Data;
using WebApplication7.Data.Interfaces;
using WebApplication7.Models;

namespace WebApplication7.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ApplicationUser GetUser(string email)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null.");
            }
            var user = _context.AspNetUsers.FirstOrDefault(u => u.Email == email);
            return user;
        }
        public ApplicationUser GetUserByUsername(string username)
        {
            var user = _context.AspNetUsers.FirstOrDefault(u => u.UserName == username);
            return user;
        }
    }
}
