using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using URLShortener.API.Services;
using URLShortener.DataAccess;
using URLShortener.DataAccess.Models;
using Xunit;

namespace URLShortener.UnitTest
{
    public class URLShortenerServiceTest : IDisposable
    {
        protected readonly URLShortenerContext _context;

        // Configuration dictionary
        Dictionary<string, string> myConfiguration = new Dictionary<string, string>
        {
            {"URLShortenerWebURL", "https://localhost:44475/"},
        };

        private IConfiguration _configuration;

        public URLShortenerServiceTest()
        {
            // DBContext using In-Memory database
            var options = new DbContextOptionsBuilder<URLShortenerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new URLShortenerContext(options);

            _context.Database.EnsureCreated();

            // Set In-Memory configuration
            _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();

        }

        [Fact]
        public async Task GetAll()
        {
            // Data
            List<URL> data = new List<URL>{
             new URL{
                 UrlID=1,
                 ProvidedURL="https://code-maze.com/unit-testing-aspnetcore-web-api/",
                 ShortURL="https://localhost:44407/BADX3",
                 ShortCode="BADX3",
                 CreationDate=DateTime.Now
             },
             new URL{
                 UrlID=2,
                 ProvidedURL="https://www.learmoreseekmore.com/2022/02/dotnet6-unit-testing-aspnetcore-web-api-using-xunit.html",
                 ShortURL="https://localhost:44407/DGDX3",
                 ShortCode="DGDX3",
                 CreationDate=DateTime.Now
             },
             new URL{
                 UrlID=3,
                 ProvidedURL="https://stackoverflow.com/questions/48061096/why-cant-i-call-the-useinmemorydatabase-method-on-dbcontextoptionsbuilder",
                 ShortURL="https://localhost:44407/FFQS3",
                 ShortCode="FFQS3",
                 CreationDate=DateTime.Now
             }
            };
            
            /// Arrange to in-memory db
            _context.URL.AddRange(data);
            _context.SaveChanges();

            // Test the service
            var urlShortenerService = new URLShortenerService(_context, _configuration);

            var result = await urlShortenerService.GetAll();


            // Assert

            Assert.IsType<URL[]>(result);

        }

        [Theory]
        [InlineData("BADX3")]
        [InlineData("DGDX3")]
        public async Task GetProvidedURL(string short_code)
        {
            // Data
            List<URL> data = new List<URL>{
             new URL{
                 UrlID=1,
                 ProvidedURL="https://code-maze.com/unit-testing-aspnetcore-web-api/",
                 ShortURL="https://localhost:44407/BADX3",
                 ShortCode="BADX3",
                 CreationDate=DateTime.Now
             },
             new URL{
                 UrlID=2,
                 ProvidedURL="https://www.learmoreseekmore.com/2022/02/dotnet6-unit-testing-aspnetcore-web-api-using-xunit.html",
                 ShortURL="https://localhost:44407/DGDX3",
                 ShortCode="DGDX3",
                 CreationDate=DateTime.Now
             },
             new URL{
                 UrlID=3,
                 ProvidedURL="https://stackoverflow.com/questions/48061096/why-cant-i-call-the-useinmemorydatabase-method-on-dbcontextoptionsbuilder",
                 ShortURL="https://localhost:44407/FFQS3",
                 ShortCode="FFQS3",
                 CreationDate=DateTime.Now
             }
            };

            /// Arrange to in-memory db
            _context.URL.AddRange(data);
            _context.SaveChanges();

            // Test the service
            var urlShortenerService = new URLShortenerService(_context, _configuration);

            var result = await urlShortenerService.GetProvidedURL(short_code);
 
            // Assert

            Assert.IsType<URL[]>(result);

        }
 

        [Theory]
        [InlineData("https://code-maze.com/unit-testing-aspnetcore-web-api/")]
        [InlineData("https://www.learmoreseekmore.com/2022/02/dotnet6-unit-testing-aspnetcore-web-api-using-xunit.html")]
        public async Task AddURL(string url)
        {
            // Test the service
            var urlShortenerService = new URLShortenerService(_context, _configuration);

            var result = await urlShortenerService.AddUrl(url);
 
            // Assert
            Assert.IsType<string>(result);

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

    }
}