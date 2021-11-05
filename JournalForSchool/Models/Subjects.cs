using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalForSchool.Models
{
    public class Subjects
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
        public Subjects()
        {
            Teachers = new List<Teacher>();
        }
    }
}
