using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using apiv2.Dtos;
using apiv2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using apiv2.Configurations;
using System.Security.Cryptography;
using apiv2.models;
using apiv2.Data;

namespace apiv2.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfigSettings _config;
        private readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

        public LoginController(ApplicationDBContext context, IConfigSettings config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // 1. Validate User Credentials
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return Unauthorized("Invalid credentials");
            }


            // 2. Generate Tokens
            var jwtToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            // 3. Store Refresh Token (hash it for security)
            refreshToken.Token = BCrypt.Net.BCrypt.HashPassword(refreshToken.Token);
            refreshToken.UserId = user.UserId;
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            SetJwtCookie("jwtToken", jwtToken);
            SetJwtCookie("refreshToken", refreshToken.Token);

            // 4. Return Both Tokens
            return Ok(new { token = jwtToken, refreshToken = refreshToken.Token });
        }

        private void SetJwtCookie(string name, string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to true in production
                // ... Adjust expiration times as needed
            };
            Response.Cookies.Append(name, token, cookieOptions);
        }

        // Helper to Generate JWT
        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_config.JwtExpirationDays));

            var token = new JwtSecurityToken(
                _config.JwtIssuer,
                _config.JwtAudience,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Helper to Generate Refresh Token
        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            _randomNumberGenerator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(_config.RefreshTokenExpirationDays),
                Created = DateTime.UtcNow
            };
        }
    }
}