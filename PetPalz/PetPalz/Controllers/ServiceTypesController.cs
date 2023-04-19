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
    public class ServiceTypesController : ControllerBase
    {
        private readonly PetPalzContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ServiceTypesController(PetPalzContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/ServiceTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceType>>> GetServiceTypes()
        {
          if (_context.ServiceTypes == null)
          {
              return NotFound();
          }
          return await _context.ServiceTypes.ToListAsync();
        }
        [HttpGet("{type}")]
        public async Task<ActionResult<IEnumerable<ServiceType>>> GetServiceTypesByUser(string type)
        {
            if (_context.ServiceTypes == null)
            {
                return NotFound();
            }
            return type == "petOwner" ? 
                await _context.ServiceTypes.Where(x => x.IsForOwner).ToListAsync() : 
                await _context.ServiceTypes.Where(x => !x.IsForOwner).ToListAsync();
        }

        // GET: api/ServiceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceType>> GetServiceType(int id)
        {
          if (_context.ServiceTypes == null)
          {
              return NotFound();
          }
          var serviceType = await _context.ServiceTypes.FindAsync(id);

            if (serviceType == null)
            {
                return NotFound();
            }

            return serviceType;
        }

        // PUT: api/ServiceTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceType(int id, ServiceType serviceType)
        {
            if (id != serviceType.Id)
            {
                return BadRequest();
            }

            _context.Entry(serviceType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceTypeExists(id))
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
        public async Task<ActionResult<ServiceType>> PostServiceType(ServiceType serviceType)
        {
          if (_context.ServiceTypes == null)
          {
              return Problem("Entity set 'PetPalzContext.ServiceTypes'  is null.");
          }
          _context.ServiceTypes.Add(serviceType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiceType", new { id = serviceType.Id }, serviceType);
        }

        // DELETE: api/ServiceTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceType(int id)
        {
            if (_context.ServiceTypes == null)
            {
                return NotFound();
            }
            var serviceType = await _context.ServiceTypes.FindAsync(id);
            if (serviceType == null)
            {
                return NotFound();
            }

            _context.ServiceTypes.Remove(serviceType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceTypeExists(int id)
        {
            return (_context.ServiceTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
