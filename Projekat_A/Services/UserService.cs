using Microsoft.Extensions.DependencyInjection;
using Projekat_A.Models;
using Projekat_A.Services;

namespace Projekat_A.Services
{
    public class UserService : BaseService
    {
        private IServiceProvider _serviceProvider;
        public UserService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void saveUserChanges(int mode, int theme, string font)
        {
            User? user = _serviceProvider.GetRequiredService<Storage>().User;

            User? user2 = _context.Users.FirstOrDefault(u => u.Id == user.Id);

            if (user2.Theme != (sbyte)theme)
                user2.Theme = (sbyte)theme;
            if (user2.Mode != (sbyte)mode)
                user2.Mode = (sbyte)mode;
            if (!user2.Font.Equals(font))
                user2.Font = font;

            _context.SaveChanges();
        }

        public void logUser(sbyte value)
        {
            User? user = _serviceProvider.GetRequiredService<Storage>().User;
            _context.Users.FirstOrDefault(u => u.Id == user.Id).Logged = value;
            _context.SaveChanges();
        }
    }
}
