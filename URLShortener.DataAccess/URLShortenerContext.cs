
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

   

    }
}
