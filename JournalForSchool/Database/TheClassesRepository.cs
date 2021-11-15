using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.DataAccessModel;
using DataAccessLayer.Models;

namespace JournalForSchool
{
    public class TheClassesRepository : IRepository<TheClasses>
    {
        private readonly DataAccessClasses dataAccess;

        public TheClassesRepository(AccessOrchestrator orchestrator = null)
        {
            if (orchestrator == null)
            {
                throw new ArgumentException();
            }
            dataAccess = orchestrator.GetModelAccess<TheClasses>() as DataAccessClasses;
        }

        public IEnumerable<TheClasses> GetAll()
        {
            return dataAccess.GetAll();
        }

        public TheClasses Get(int id)
        {
            return dataAccess.Get(id);
        }

        public void Create(TheClasses theClass)
        {
            dataAccess.Create(theClass);
        }

        public void Update(TheClasses theClass)
        {
            dataAccess.Update(theClass);
        }
            
        public void Delete(int id)
        {
            dataAccess.Delete(id);
        }

        public List<int> GetTheDisctinctClassesNames()
        {
            return dataAccess.GetDistinctClassesNames();
        }

        public List<string> GetTheClassesLetters(int TheClass)
        {
            return dataAccess.GetClassesLetters(TheClass);
        }

        public int GetTheClassByNumber(int TheClass, string letter)
        {
            var model = new TheClasses()
            {
                TheClass = TheClass,
                ClassLetter = letter
            };
            return dataAccess.GetClassByNumber(model).Id;
        }

        public TheClasses GetTheClassById(int class_id, MainWindow mainWindow = null)
        {
            return dataAccess.Get(class_id);
        }

        public TheClasses GetTheClassByFullName(string full_class_name)
        {
            TheClasses needClass = null;

            foreach (var item in GetAll())
            {
                if (item.TheClass + " " + item.ClassLetter == full_class_name) 
                    needClass = item;
            }
            return needClass;
        }
    }
}
