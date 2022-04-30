using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShortener.DataAccess;
using URLShortener.API;
using URLShortener.DataAccess.Models;

namespace URLShortener.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class URLShortenerController : ControllerBase
    {

        private URLShortenerContext _context;
        private IConfiguration _configuration;
        private readonly ILogger<URLShortenerController> _logger;
        
        
        public URLShortenerController(URLShortenerContext context, IConfiguration configuration, ILogger<URLShortenerController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet(Name = "GetURLShortener")]
        public async Task<ActionResult<List<URL>>> Get()
        {
            try
            {
                return Ok(await _context.URL.ToListAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            } 
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddURL([FromBody]string url)
        {
            try
            {
                string short_code = Util.GetShortCode();
        
                // Get the setting value of URLShortenerWebURL.
                string web_url = _configuration.GetValue<string>("URLShortenerWebURL");
                // Contact the generated short code.
                string short_url = web_url + short_code;
                URL _url = new URL
                {
                    ProvidedURL = url,
                    ShortURL = short_url,
                    ShortCode = short_code
                };
                _context.URL.Add(_url);
                await _context.SaveChangesAsync();
                return short_url;            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        [HttpPut(Name ="UpdateVisits")]
        public async Task<ActionResult> UpdateVisits(URL url)
        {
            try
            {
                // Find URL by id
                URL _url = await _context.URL.FirstAsync(x => x.UrlID == url.UrlID);
                if (url == null)
                    BadRequest("URL not found.");

                // Increate visits count
                _url.Visits += 1;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

    }
}