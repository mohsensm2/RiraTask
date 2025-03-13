using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace TestRira.Core.Entities
{
    [Table("Person")]
    public partial class Person
    {
        public int Id { get; set; }
        public string?  Name { get; set; }
        public string? LastName { get; set; }
        public string? NationCode { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
