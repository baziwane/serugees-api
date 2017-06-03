using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serugees.Api.Models
{
    public class SerugeesContext: DbContext
    {
         public SerugeesContext(DbContextOptions<SerugeesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}