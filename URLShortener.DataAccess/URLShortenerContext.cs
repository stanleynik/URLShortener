
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccess.Models;

namespace URLShortener.DataAccess
{
    public class URLShortenerContext:DbContext
    {
        public URLShortenerContext(DbContextOptions<URLShortenerContext> options) : base(options)
        {
            
        }

        public DbSet<User> User { get; set; }
        public DbSet<URL> URL { get; set; }
        public DbSet<SessionToken> SessionToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Default user
            modelBuilder.Entity<User>()
            .HasData(
                new User
                {
                    UserID = 1,
                    Username = "admin",
                    Password = "admin",
                    CreationDate = DateTime.Now
                }
                );

        }

    }
}
