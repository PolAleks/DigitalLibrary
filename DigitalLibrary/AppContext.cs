using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary
{
    public class AppContext : DbContext
    {
        // Объекты таблицы User
        public DbSet<User> Users { get; set; }
        
        // Объекты таблицы Book
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Database=HW;Trusted_Connection=True; TrustServerCertificate=true;");
        }
    }
}
