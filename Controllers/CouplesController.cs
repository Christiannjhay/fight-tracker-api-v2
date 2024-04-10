using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiv2.Data; // Replace with your data context namespace

namespace apiv2.Controllers
{
    [Route("api/couple")]
    [ApiController]
    public class CouplesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CouplesController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCouplesByUserId(int userId)
        {
            var coupleData = await _context.Couples
                .Where(c => c.Couple_1 == userId || c.Couple_2 == userId)
                .Select(c => new {
                    CoupleCode = c.CoupleCode,
                    Couple_1_Name = _context.Users.Where(u => u.UserId == c.Couple_1).Select(u => u.Name).FirstOrDefault(),
                    Couple_2_Name = _context.Users.Where(u => u.UserId == c.Couple_2).Select(u => u.Name).FirstOrDefault()
                })
                .FirstOrDefaultAsync(); 

            if (coupleData == null)
            {
                return NotFound();
            }

            return Ok(coupleData);
        }
    }
}