using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace JournalForSchool
{
    public class TheClassesRepository : IRepository<TheClasses>
    {
        public UnitOfWork unitOfWork;
        public Context db;

        public TheClassesRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
            this.db = unitOfWork.Db;
            
        }

        public IEnumerable<TheClasses> GetAll()
        {
            return db.TheClasses;
        }

        public TheClasses Get(int id)
        {
            return db.TheClasses.Find(id);
        }

        public void Create(TheClasses theClass)
        {
            db.TheClasses.Add(theClass);
        }

        public void Update(TheClasses theClass)
        {
            db.Entry(theClass).State = EntityState.Modified;
        }
            
        public void Delete(int id)
        {
            TheClasses theClass = db.TheClasses.Find(id);
            if (theClass != null)
                db.TheClasses.Remove(theClass);
        }

        public List<int> GetTheDisctinctClassesNames()
        {
            var allClases = db.TheClasses.Select(item => item.TheClass).Distinct().ToList();
            allClases.Sort();
            allClases.Reverse();

            return allClases;
        }

        public List<string> GetTheClassesLetters(int TheClass)
        {
            var allLetters = db.TheClasses.ToList().Where(item => item.TheClass == TheClass).Select(item => item.ClassLetter).OrderBy(item => item);
            return allLetters.ToList();
        }

        public int GetTheClassByNumber(int TheClass, string letter)
        {
            var Class = db.TheClasses.FirstOrDefault(item => item.TheClass == TheClass && item.ClassLetter == letter);
            return Class.Id;
        }

        public TheClasses GetTheClassById(int class_id, MainWindow mainWindow = null)
        {
            return Context.GetInstance().TheClasses.FirstOrDefault(item => item.Id == class_id);
        }

        public TheClasses GetTheClassByFullName(string full_class_name)
        {
            TheClasses needClass = null;

            foreach (var item in db.TheClasses.ToList())
            {
                if (item.TheClass + " " + item.ClassLetter == full_class_name) 
                    needClass = item;
            }
            return needClass;
        }
    }
}
