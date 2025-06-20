using Projekat_A.Util;
using Projekat_A.Models;

namespace Projekat_A.Services
{
    public class LoginService : BaseService
    {
        public LoginService() { }

        public async Task<User?> logUser(string username, string password)
        {
            await Task.Delay(500);
            User? user = _context.Users.FirstOrDefault(user => user.Username.Equals(username) && user.Password.Equals(DBUtil.hashPassword(password)));

            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}

