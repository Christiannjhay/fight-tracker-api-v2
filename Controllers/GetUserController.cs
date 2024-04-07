using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Data;
using apiv2.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiv2.Controllers
{
    [Route("api/users")]
    [ApiController] 
    public class GetUserController : ControllerBase
    {
         private readonly ApplicationDBContext _context;

        public GetUserController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() 
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return Ok(users); 
            } 
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving users: "+ ex); 
            }
        }
        
    }
}