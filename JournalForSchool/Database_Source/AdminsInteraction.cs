using System.Linq;
using DataAccessLayer.Models;

namespace JournalForSchool.Database_Source
{
    public static class AdminsInteraction
    {
        private static UnitOfWork unitOfWork = UnitOfWork.GetInstance();
        public static bool IsAdmin(User user)
        {
            
            var admin = unitOfWork.Db.Admins.FirstOrDefault(item => item.UserId == user.Id);

            if (admin == null) return false;
            else return true;
            
        }
    }
}
