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

        [HttpGet]
        public async Task<ActionResult<URL[]>> Get()
        {
            try
            {
                return Ok(await _context.URL.ToArrayAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            } 
        }


        [HttpGet]
        [Route("/GetProvidedURL/{short_code?}")]
        public async Task<ActionResult<string>> GetProvidedURL([FromRoute] string short_code)
        {
            try
            {
                URL url = await _context.URL.FirstAsync(x => x.ShortCode == short_code);
                if (url == null)
                    return BadRequest("URL not found.");

                return url.ProvidedURL;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("/GetTop20")]
        public async Task<ActionResult<URL[]>> GetTop20()
        {
            try
            {
                URL[] urls = await _context.URL.OrderByDescending(x => x.Visits).Take(20).ToArrayAsync();
                if (urls == null)
                    return BadRequest("URL not found.");

                return urls;
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
                // Validate URL
                Uri? uriResult;
                bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (result == false)
                    return BadRequest("URL is invalid");

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

        [HttpPost]
        [Route("/UpdateVisits/{short_code?}")]
        public async Task<ActionResult> UpdateVisits([FromRoute] string short_code)
        {
            try
            {
                // Find URL by id
                URL _url = await _context.URL.FirstAsync(x => x.ShortCode == short_code);
                if (_url == null)
                    return BadRequest("URL not found.");

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