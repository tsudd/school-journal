using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalForSchool.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? TheClassesId { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ImagePath { get; set; }

        public virtual TheClasses TheClasses { get; set; }
        public Teacher Teacher { get; set; }

        public virtual ICollection<Mark> Marks { get; set; }
        public User()
        {
            Marks = new List<Mark>();
        }
    }
}

