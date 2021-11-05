using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalForSchool.Models
{
    public class Teacher
    {
        [Key, ForeignKey("User")]
        public int Id { get; set; }
        //public int UserId { get; set; }
        public int SubjectId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual Subjects Subject { get; set; }
    }
}
