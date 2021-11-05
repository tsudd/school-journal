using System;
using System.Data.Entity;
using System.Windows;
using JournalForSchool.Models;
using Renci.SshNet.Messages;

namespace JournalForSchool
{
    public class Context : DbContext
    {
        private static Context instance;

        private Context()
            : base("DbConnection")
        { }

        public static Context GetInstance()
        {
            if (instance == null)
            {
                instance = new Context();
                instance.LoadDataSource();
            }
            return instance;
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TheClasses> TheClasses { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Timetable> Timetable { get; set; }
        public DbSet<Admin> Admins { get; set; }


        public void LoadDataSource()
        {
            this.Teachers.Load();
            this.TheClasses.Load();
            this.Subjects.Load();
            this.Timetable.Load();
            this.Users.Load();
            this.Marks.Load();
            this.Admins.Load();
        }
    }
}
