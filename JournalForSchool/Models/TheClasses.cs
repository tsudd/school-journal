using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalForSchool.Models
{
    public class TheClasses
    {
        public int Id { get; set; }
        public int TheClass { get; set; }
        public string ClassLetter { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public TheClasses()
        {
            Users = new List<User>();
        }
    }
}
