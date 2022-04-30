using Microsoft.AspNetCore.Mvc;

namespace URLShortener.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class URLShortenerController : ControllerBase
    {
       
        private readonly ILogger<URLShortenerController> _logger;

        public URLShortenerController(ILogger<URLShortenerController> logger)
        {
            _logger = logger;
        }
 
    }
}