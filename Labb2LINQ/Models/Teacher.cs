using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2LINQ.Models
{
    public class Teacher
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public ICollection<Subject> Subjects { get; set; }

    }
}
