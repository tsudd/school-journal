using JournalForSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JournalForSchool
{
    public class UnitOfWork : IDisposable
    {
        public Context Db;

        private TeachersRepository teachersRepository;
        private SubjectsRepository subjectsRepository;
        private TheClassesRepository theClassesRepository;
        private TimetableRepository timetableRepository;
        private UsersRepository usersRepository;
        private MarksRepository marksRepository;
        private AdminsRepository adminsRepository;

        private static UnitOfWork instance;

        private UnitOfWork()
        {
 
        }

        public static UnitOfWork GetInstance()
        {
            if (instance == null)
            {
                instance = new UnitOfWork();
                instance.Db = Context.GetInstance();
            }
            return instance;
        }

        public TeachersRepository Teachers
        {
            get
            {
                if (teachersRepository == null)
                    teachersRepository = new TeachersRepository(Db);
                return teachersRepository;
            }
        }

        public SubjectsRepository Subjects
        {
            get
            {
                if (subjectsRepository == null)
                    subjectsRepository = new SubjectsRepository();
                return subjectsRepository;
            }
        }

        public TheClassesRepository TheClasses
        {
            get
            {
                if (theClassesRepository == null)
                    theClassesRepository = new TheClassesRepository();
                return theClassesRepository;
            }
        }

        public TimetableRepository Timetable
        {
            get
            {
                if (timetableRepository == null)
                    timetableRepository = new TimetableRepository(Db);
                return timetableRepository;
            }
        }

        public UsersRepository Users
        {
            get
            {
                if (usersRepository == null)
                    usersRepository = new UsersRepository();
                return usersRepository;
            }
        }

        public MarksRepository Marks
        {
            get
            {
                if (marksRepository == null)
                    marksRepository = new MarksRepository(Db);
                return marksRepository;
            }
        }

        public AdminsRepository Admins
        {
            get
            {
                if (adminsRepository == null)
                    adminsRepository = new AdminsRepository(Db);
                return adminsRepository;
            }
        }

        public void Save()
        {
            Db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            // Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
