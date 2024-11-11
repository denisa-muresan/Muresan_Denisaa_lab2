using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Muresan_Denisaa_lab2.Models;

namespace Muresan_Denisaa_lab2.Data
{
    public class Muresan_Denisaa_lab2Context : DbContext
    {
        public Muresan_Denisaa_lab2Context (DbContextOptions<Muresan_Denisaa_lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Muresan_Denisaa_lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Muresan_Denisaa_lab2.Models.Publisher> Publisher { get; set; } = default!;

        public DbSet<Muresan_Denisaa_lab2.Models.Author> Authors { get; set; } = default!;
        public DbSet<Muresan_Denisaa_lab2.Models.Category> Category { get; set; } = default!;
        public DbSet<Muresan_Denisaa_lab2.Models.Member> Member { get; set; } = default!;
        public DbSet<Muresan_Denisaa_lab2.Models.Borrowing> Borrowing { get; set; } = default!;
    }
}
