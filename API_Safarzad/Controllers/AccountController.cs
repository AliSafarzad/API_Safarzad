using IInterfacse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Safarzad.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IBook _ibook;
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration,IBook book)
        {
            _configuration = configuration;
            _ibook = book;
        }


        [HttpPost]
        public IActionResult Login(User user)
        {
            Result result = new Result();

            result = _ibook.CheckUserPassword(user);

            if (result.Success==true)
            {
               result.Token = GenerateJwtToken(user.username);
            }
            return Ok(result);
        }

        private string GenerateJwtToken(string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var secretKey = jwtSettings["SecretKey"];
            var key = Encoding.UTF8.GetBytes(secretKey);
            var issuer = jwtSettings["ValidIssuer"];
            var audience = jwtSettings["ValidAudience"];

            var claims = new[]
            {
            new Claim("UserName", username),
        };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
