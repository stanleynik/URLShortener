# URLShortener
URL shortener allows you to create shortened URLs and records how many times the short URL was visited.

URLShortener.DataAccess project is using Entity Framework Code First Approach to create the URLShortenerContext that is used for the API.

To generate the database you need to install the EF dotnet tools that allows you to create and run migrations:

- Install the EF dotnet tools executing this command in Package Manager Console
dotnet tool install --global dotnet-ef
- To create and run migrations use:
dotnet ef migrations add <<name>>
dotnet ef database update

URLShortener.API is the Web API and exposes methods
  - To create a short url from a provided url 
  - Get provided url from a generated short code
  - Increase count of visits to a short url
  - Get the Top 20 most visited short urls
  
URLShortener.Web is the Angular site that consumes the API
