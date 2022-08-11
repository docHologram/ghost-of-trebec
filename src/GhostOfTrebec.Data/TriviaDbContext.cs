using GhostOfTrebec.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostOfTrebec.Data
{
    public class TriviaDbContext : DbContext
    {
        public TriviaDbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProblemConfiguration());
        }
    }
}
