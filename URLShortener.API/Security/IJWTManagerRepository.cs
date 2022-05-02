using URLShortener.DataAccess.Models;

namespace URLShortener.API.Security
{
   
        public interface IJWTManagerRepository
        {
            SessionToken Authenticate(User user);
        }
 
}
