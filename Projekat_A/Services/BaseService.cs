using Projekat_A.Data;
using Projekat_A.Util;

namespace Projekat_A.Services
{
    public class BaseService
    {
        protected FitnessCenterContext _context;

        public BaseService()
        {
            try
            {
                _context = DBUtil.getContext();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GREŠKA: " + ex.Message);
                Console.WriteLine("DETALJI: " + ex.InnerException?.Message);
                Console.WriteLine("STACK TRACE: " + ex.StackTrace);
            }
        }
    }
}
