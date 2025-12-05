using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CodeFirstRestApi.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CodeFirstRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        // POST {loginModel}
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null) return BadRequest("Invalid client request.");


            if (!VerifyUserCredentials(loginModel)) return Unauthorized("U R not authorized.");

            // TODO retrieve user from persistence store
            _logger.LogInformation($"User: {loginModel.UserName} PW: {loginModel.Password}");


            var signingKey = _configuration["Jwt:SigningKey"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var expiryDateTime = DateTime.Now.AddMinutes(5);
            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new List<Claim>(),
                expires: expiryDateTime,
                signingCredentials: signingCredentials);


            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new {Token = tokenString, ExpiryDateTime = expiryDateTime});
        }

        private static bool VerifyUserCredentials(LoginModel loginModel)
        {
            return ("abc$1".Equals(loginModel.UserName) && "cba$2".Equals(loginModel.Password));
        }
    }
}