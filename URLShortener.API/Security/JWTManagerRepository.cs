using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using URLShortener.DataAccess.Models;

namespace URLShortener.API.Security
{
	public class JWTManagerRepository : IJWTManagerRepository
	{
 
		private readonly IConfiguration Configuration;
		// Needed to use URLShortenerContext in singleton 
		private readonly IServiceScopeFactory scopeFactory;

		public JWTManagerRepository(IConfiguration iconfiguration, IServiceScopeFactory scopeFactory)
		{
			this.Configuration = iconfiguration;
			this.scopeFactory = scopeFactory;
		}
		public SessionToken Authenticate(User user) {
			using (var scope = scopeFactory.CreateScope())
			{
				var _context = scope.ServiceProvider.GetRequiredService<URLShortenerContext>();
				// Validate User
				var current_user = _context.User.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
				if (current_user == null)
					return null;

			}
		
	 
			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
			  {
			 new Claim(ClaimTypes.Name, user.Username)
			  }),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var sessionToken = new SessionToken
			{
				Token = tokenHandler.WriteToken(token),
				User = user
			};

			return new SessionToken { Token = tokenHandler.WriteToken(token) };

		}
	}
}
