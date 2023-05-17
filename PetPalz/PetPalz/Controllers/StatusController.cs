using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPalz.Data;
using PetPalz.Models;

namespace PetPalz.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    private PetPalzContext _context;
    private IConfiguration _configuration;
    private UserManager<IdentityUser> _userManager;
    private RoleManager<IdentityRole> _roleManager;

    public StatusController(PetPalzContext context, IConfiguration configuration, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet("{id}")]
    public ActionResult GetStatus( string id )
    {
        UserStatus? status = _context.UserStatuses.AsEnumerable().FirstOrDefault(x => x.UserId == id);
        if (status == null)
            return BadRequest();
        return Ok(status);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStatus(string id, string statusMessage)
    {
        var status = _context.UserStatuses.AsEnumerable().First(x => x.UserId == id);
        if (status == null)
        {
            return BadRequest();
        }

        status.Name = statusMessage;
        _context.UserStatuses.Update(status);
        await _context.SaveChangesAsync();
        return Ok("Status Update");
    }
}