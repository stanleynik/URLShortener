using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShortener.DataAccess;
using URLShortener.DataAccess.Models;

namespace URLShortener.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class URLShortenerController : ControllerBase
    {
    
        private readonly ILogger<URLShortenerController> _logger;
        private URLShortenerContext _context;
        public URLShortenerController(URLShortenerContext context, ILogger<URLShortenerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet(Name = "GetURLShortener")]
        public async Task<ActionResult<List<URL>>> Get()
        {
            try
            {

                return Ok(await _context.URL.ToListAsync());
                //using (var ctx = new URLShortenerContext())
                //{
                //    var newURL = new URL() { 
                //        ProvidedURL = "https://docs.microsoft.com/en-us/aspnet/web-api/overview/getting-started-with-aspnet-web-api",
                //        ShortURL = "https://localhost:4200/AX323",
                //        ShortCode = "",
                //        Visits = 1
                //    };

               
                //    ctx.URL.Add(newURL);
                //    ctx.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        
            
        }
    }
}