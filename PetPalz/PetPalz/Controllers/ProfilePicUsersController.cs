using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPalz.Data;
using PetPalz.Models;

namespace PetPalz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilePicUsersController : ControllerBase
    {
        private readonly PetPalzContext _context;

        public ProfilePicUsersController(PetPalzContext context)
        {
            _context = context;
        }

        // GET: api/ProfilePicUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfilePicUser>>> GetProfilePicUsers()
        {
          if (_context.ProfilePicUsers == null)
          {
              return NotFound();
          }
            return await _context.ProfilePicUsers.ToListAsync();
        }

        // GET: api/ProfilePicUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfilePicUser>> GetProfilePicUser(int id)
        {
          if (_context.ProfilePicUsers == null)
          {
              return NotFound();
          }
            var profilePicUser = await _context.ProfilePicUsers.FindAsync(id);

            if (profilePicUser == null)
            {
                return NotFound();
            }

            return profilePicUser;
        }

        // PUT: api/ProfilePicUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfilePicUser(int id, ProfilePicUser profilePicUser)
        {
            if (id != profilePicUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(profilePicUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfilePicUserExists(id))
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

        // POST: api/ProfilePicUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProfilePicUser>> PostProfilePicUser(ProfilePicUser profilePicUser)
        {
          if (_context.ProfilePicUsers == null)
          {
              return Problem("Entity set 'PetPalzContext.ProfilePicUsers'  is null.");
          }
            _context.ProfilePicUsers.Add(profilePicUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfilePicUser", new { id = profilePicUser.Id }, profilePicUser);
        }

        // DELETE: api/ProfilePicUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfilePicUser(int id)
        {
            if (_context.ProfilePicUsers == null)
            {
                return NotFound();
            }
            var profilePicUser = await _context.ProfilePicUsers.FindAsync(id);
            if (profilePicUser == null)
            {
                return NotFound();
            }

            _context.ProfilePicUsers.Remove(profilePicUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfilePicUserExists(int id)
        {
            return (_context.ProfilePicUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
