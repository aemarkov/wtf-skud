using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using skud.Domain.Models;

namespace skud.Data
{
    public class SkudContext : DbContext
    {
        public SkudContext():base("DbConnection")
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }
    }
}
