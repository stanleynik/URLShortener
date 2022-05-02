using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShortener.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using URLShortener.API.Services;

namespace URLShortener.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class URLShortenerController : ControllerBase
    {

        private URLShortenerContext _context;
        private readonly IURLShortenerService _service;
        private IConfiguration _configuration;
 
        public URLShortenerController(URLShortenerContext context, IURLShortenerService service, IConfiguration configuration)
        {
            _context = context;
            _service = service;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<URL[]>> Get()
        {
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500);
            } 
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/GetProvidedURL/{short_code?}")]
        public async Task<ActionResult<string>> GetProvidedURL([FromRoute] string short_code)
        {
            try
            {
                URL url = await _service.GetProvidedURL(short_code);
                if (url == null)
                    return BadRequest("URL not found");

                return url.ProvidedURL;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("/GetTop20")]
        public async Task<ActionResult<URL[]>> GetTop20()
        {
            try
            {
                URL[] urls = await _service.GetTop20();
                return urls;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> AddURL([FromBody]string url)
        {
            try
            {
                return await _service.AddUrl(url);          
            }
            catch (Exception ex)
            {
                if(ex.Message == "URL is invalid")
                    return BadRequest(ex.ToString());

                Console.WriteLine(ex.ToString());
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("/UpdateVisits/{short_code?}")]
        public async Task<ActionResult> UpdateVisits([FromRoute] string short_code)
        {
            try
            {
                bool result = await _service.UpdateVisits(short_code);
               
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500);
            }
        }

    }
}