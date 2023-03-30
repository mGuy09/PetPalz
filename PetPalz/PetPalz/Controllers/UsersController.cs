using System.Drawing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PetPalz.Data;
using PetPalz.Models;
using PetPalz.Models.Dtos;

namespace PetPalz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private PetPalzContext _context;
        private IConfiguration _configuration;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UsersController(PetPalzContext context, IConfiguration configuration, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(new PersonalUserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == user.Id).FirstName,
                    LastName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == user.Id).LastName,
                    YearsOfExperience = _context.UserYearsOfExperience.AsEnumerable().First(x => x.UserId == user.Id),
                    PhoneNumber = user.PhoneNumber,
                    ProfilePicUrl = _context.ProfilePicUsers.AsEnumerable().First(x => x.UserId == user.Id).ImageUrl,
                    Qualifications = _context.QualificationsInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                        .Select(x => _context.Qualifications.AsEnumerable().First(y => y.Id == x.QualificationId))
                        .ToList(),
                    Rating = _context.UserRatings.AsEnumerable().First(x => x.UserId == user.Id),
                    ServiceType = _context.ServiceTypeInUsers.AsEnumerable().Where(x => x.UserId == user.Id).Select(x =>
                        _context.ServiceTypes.AsEnumerable().First(y => y.Id == x.ServiceTypeId)).First(),
                    UserName = user.UserName,
                    UserType = _context.UserTypesInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                        .Select(x => _context.UserTypes.AsEnumerable().First(y => y.Id == x.UserTypeId)).First(),
                    Roles = roles.ToList()
                });
            }
            return BadRequest(new {Message = "Not logged in"});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return BadRequest(new { Message = "User does not exist" });
            return Ok(new UserDto
            {
                Id = id,
                UserName = user.UserName,
                FirstName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == id).FirstName,
                LastName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == id).LastName,
                Qualifications = _context.QualificationsInUsers.AsEnumerable().Where(x => x.UserId == id)
                    .Select(x => _context.Qualifications.First(y => y.Id == x.QualificationId)).ToList(),
                YearsOfExperience = _context.UserYearsOfExperience.AsEnumerable().First(x => x.UserId == id),
                PhoneNumber = user.PhoneNumber,
                ProfilePicUrl = _context.ProfilePicUsers.AsEnumerable().First(x => x.UserId == id).ImageUrl,
                Rating = _context.UserRatings.AsEnumerable().First(x => x.UserId == id),
                ServiceType = _context.ServiceTypeInUsers.AsEnumerable().Where(x => x.UserId == id)
                    .Select(x => _context.ServiceTypes.AsEnumerable().First(y => y.Id == x.ServiceTypeId)).First(),
                UserType = _context.UserTypesInUsers.AsEnumerable().Where(x => x.UserId == id)
                    .Select(x => _context.UserTypes.AsEnumerable().First(y => y.Id == x.UserTypeId)).First()
            });
        }

        [HttpPost]
        public async Task<ActionResult> AddUserFullName(string firstName, string lastName, string userId)
        {
            await _context.UserFullNames.AddAsync(new UserFullName
                { FirstName = firstName, LastName = lastName, UserId = userId });
            await _context.SaveChangesAsync();
            return Ok(new {Message = "Entity Created"});
        }
    }
}
