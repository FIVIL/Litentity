using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace FIVIL.Litentity
{
    public class LitentityContext : DbContext
    {
        public LitentityContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<LitentityUsers> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<LitentityUsers>(e =>
                e.HasIndex(p => p.UserName).IsUnique());

            modelBuilder.Entity<LitentityUsers>(e =>
                e.HasIndex(p => p.EmailAddress).IsUnique());

            modelBuilder.Entity<LitentityUsers>(e =>
                e.HasIndex(p => p.PhoneNumber).IsUnique());

            base.OnModelCreating(modelBuilder);
        }


    }
}
