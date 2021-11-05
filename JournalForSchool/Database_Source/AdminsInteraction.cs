using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
