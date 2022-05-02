using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URLShortener.API.Security;
using URLShortener.DataAccess.Models;

namespace URLShortener.API.Controllers
{
    public class UserController : Controller
    {

		private readonly IJWTManagerRepository _jWTManager;

		public UserController(IJWTManagerRepository jWTManager)
		{
			this._jWTManager = jWTManager;
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("authenticate")]
		public IActionResult Authenticate([FromBody]User user)
		{
            try
            {
				var token = _jWTManager.Authenticate(user);

				if (token == null)
					return Unauthorized();

				return Ok(token);
			}
			catch (Exception ex)
            {
				Console.WriteLine(ex.ToString());
				return StatusCode(500);
			}
	
		}
	}
}
