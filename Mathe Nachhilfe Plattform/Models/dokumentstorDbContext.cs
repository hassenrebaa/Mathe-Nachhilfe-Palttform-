 using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Mathe_Nachhilfe_Plattform.Models
{
    public class dokumentstorDbContext:DbContext
    {
        public dokumentstorDbContext(DbContextOptions<dokumentstorDbContext> Options) : base(Options)
        {

        }
        
        public DbSet<user> users { get; set; }
        public DbSet<dokument> Dokuments { get; set; }
    }
}
