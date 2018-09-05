using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIVIL.Litentity;
using Microsoft.EntityFrameworkCore;

namespace Test.Models
{
    public class DBContext:LitentityContext
    {
        public DBContext(DbContextOptions<DBContext> options):
            base(options)
        {

        }

        public DbSet<Class> Classes { get; set; }
    }
}
