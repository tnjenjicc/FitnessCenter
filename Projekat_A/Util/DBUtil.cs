using Microsoft.EntityFrameworkCore;
using Projekat_A.Data;
using Projekat_A.Models;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Projekat_A.Util
{
    public class DBUtil
    {
        public static FitnessCenterContext getContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["fitness_center_hci"].ConnectionString;

            var optionsBuilder = new DbContextOptionsBuilder<FitnessCenterContext>();
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));
            return new FitnessCenterContext(optionsBuilder.Options);
        }

        public static String hashPassword(String password)
        {
            using (var hash = SHA256.Create())
            {
                var byteArray = Encoding.UTF8.GetBytes(password);
                var hashedResult = hash.ComputeHash(byteArray);

                string securedPass = string.Concat(Array.ConvertAll(hashedResult, h => h.ToString("X2")));
                return securedPass;
            }
        }
    }
}