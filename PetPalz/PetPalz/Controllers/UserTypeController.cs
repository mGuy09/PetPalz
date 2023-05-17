using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPalz.Data;
using PetPalz.Models;

namespace PetPalz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly PetPalzContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserTypeController( PetPalzContext context, UserManager<IdentityUser> userManager )
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserType>>> GetUserTypes()
        {
            if (_context.UserTypes == null)
            {
                return NotFound();
            }
            return await _context.UserTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserType>> GetUserType( int id )
        {
            if (_context.UserTypes == null)
            {
                return NotFound();
            }
            var userType = await _context.UserTypes.FindAsync(id);

            if (userType == null)
            {
                return NotFound();
            }

            return userType;
        }

        // PUT: api/ServiceTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserType( int id, UserType userType )
        {
            if (id != userType.Id)
            {
                return BadRequest();
            }

            _context.Entry(userType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ServiceTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserType>> PostUserType( UserType userType )
        {
            if (_context.UserTypes == null)
            {
                return Problem("Entity set 'PetPalzContext.UserTypes'  is null.");
            }
            _context.UserTypes.Add(userType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserType", new { id = userType.Id }, userType);
        }

        // DELETE: api/ServiceTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserType( int id )
        {
            if (_context.UserTypes == null)
            {
                return NotFound();
            }
            var userType = await _context.UserTypes.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }

            _context.UserTypes.Remove(userType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserTypeExists( int id )
        {
            return (_context.UserTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
