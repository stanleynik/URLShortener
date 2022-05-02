using Microsoft.EntityFrameworkCore;
using URLShortener.DataAccess.Models;

namespace URLShortener.API.Services
{
    public interface IURLShortenerService
    {
        Task<URL[]> GetAll();
        Task<URL> GetProvidedURL(string short_code);
        Task<URL[]> GetTop20();
        Task<string> AddUrl(string url);
        Task<bool> UpdateVisits(string short_code);

    }

    public class URLShortenerService : IURLShortenerService
    {
        private URLShortenerContext _context;
        private IConfiguration _configuration;

        public URLShortenerService(URLShortenerContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }
 
        public async Task<URL[]> GetAll()
        {
            return await _context.URL.ToArrayAsync();
        }

        public async Task<URL> GetProvidedURL(string short_code)
        {
            URL url = await _context.URL.FirstAsync(x => x.ShortCode == short_code);
            
            return url;
        }

        public async Task<URL[]> GetTop20()
        {
            URL[] urls = await _context.URL.OrderByDescending(x => x.Visits).Take(20).ToArrayAsync();
            return urls;
        }
 
        public async Task<string> AddUrl(string url)
        {
            // Validate URL
            Uri? uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result == false)
                throw new Exception("URL is invalid");

            string short_code = Util.GetShortCode();

            // Get the setting value of URLShortenerWebURL.
            string web_url = _configuration.GetValue<string>("URLShortenerWebURL");

            // Concat the generated short code.
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

        public async Task<bool> UpdateVisits(string short_code)
        {
            // Find URL by short_code
            URL _url = await _context.URL.FirstAsync(x => x.ShortCode == short_code);
            if (_url == null)
                throw new Exception("URL not found");

            // Increate visits count
            _url.Visits += 1;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
