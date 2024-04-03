using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Data;
using apiv2.models;
using Microsoft.AspNetCore.Mvc;
using apiv2.Dtos;
using BCrypt.Net; // Install Bcrypt.Net-Next NuGet Package

namespace apiv2.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public RegistrationController(ApplicationDBContext context)
        {
            _context = context;
        }

        private static int GenerateRandomCoupleCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationDto userDto)
        {
            if (userDto.IsCreatingCouple || userDto.CoupleCode == 0)
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                
                var user = new User
                {
                    Name = userDto.Name,
                    Username = userDto.Username,
                    Password = hashedPassword,
                    Birthday = userDto.Birthday
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var couple = new Couple
                {
                    Couple_1 = user.UserId,
                    Couple_2 = -1,
                    Anniversary = userDto.Anniversary,
                    CoupleCode = GenerateRandomCoupleCode()
                };

                _context.Couples.Add(couple);
                _context.SaveChanges();

            }
            else
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

                var user = new User
                {
                    Name = userDto.Name,
                    Username = userDto.Username,
                    Password = hashedPassword,
                    Birthday = userDto.Birthday
                };
                 _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var existingCouple = _context.Couples.FirstOrDefault(c => c.CoupleCode == userDto.CoupleCode);
                if (existingCouple != null)
                {

                    existingCouple.Couple_2 = user.UserId;
                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Couple updated successfully" });
                }
                else
                {
                    return BadRequest("Couple with code " + userDto.CoupleCode + " not found.");
                }
            }

            return Ok("Created new user and couple");

        }
    }
}