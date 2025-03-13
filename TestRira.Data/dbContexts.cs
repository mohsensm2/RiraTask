using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRira.Core.Entities;

namespace TestRira.Data
{
    public partial class dbContexts: DbContext
    {
        public dbContexts(DbContextOptions<dbContexts> options):base(options)
        {
                
        }
        public virtual DbSet<Person> Person { get; set; }
    }
}
