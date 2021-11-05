using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JournalForSchool.Database_Source
{
    public static class TheClassesInteraction
    {
        public static List<int> GetAllClassesNames()
        {
            List<int> allClasses = new List<int> { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            return allClasses;
        }

        public static List<string> GetTheClassesLettersByName(int ClassName)
        {
            var unitOfWork = UnitOfWork.GetInstance();
            
            var classesList = unitOfWork.TheClasses.GetTheClassesLetters(ClassName);
            var allClassesList = new List<string> { "А", "Б", "В", "Г", "Д", "Е", "Ж" };
            var listForReturn = new List<string>();

            foreach (var item in allClassesList)
            {
                bool isContains = false;
                foreach (var itemList in classesList)
                {
                    if (itemList.Substring(0, 1) == item.Substring(0, 1)) isContains = true;
                }

                if (isContains == false) listForReturn.Add(item);
            }
            return listForReturn;
        }
    }
}
