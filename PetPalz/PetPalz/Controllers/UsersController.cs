using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        public UsersController( PetPalzContext context, IConfiguration configuration,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("CurrentUser")]
        public async Task<ActionResult<PersonalUserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                return BadRequest(new { Message = "Not logged in" });

            
            var roles = await _userManager.GetRolesAsync(user);
            var firstName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == user.Id).FirstName;
            var lastName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == user.Id).LastName;
            string? ppu =_context.ProfilePicUsers.Count() != 0 ? _context.ProfilePicUsers.AsEnumerable().FirstOrDefault(x => x.UserId == user.Id).ImageUrl : null;
            var rating = _context.UserRatings.AsEnumerable().First(x => x.UserId == user.Id);
            var serviceType = _context.ServiceTypeInUsers.AsEnumerable().Where(x => x.UserId == user.Id).Select(x =>
                _context.ServiceTypes.AsEnumerable().First(y => y.Id == x.ServiceTypeId)).First();
            var userType = _context.UserTypesInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                .Select(x => _context.UserTypes.AsEnumerable().First(y => y.Id == x.UserTypeId)).First().Name;
            var description = _context.UserDescriptions.AsEnumerable().First(x => x.UserId == user.Id).Description;
            int yoe;
            List<Qualification>? qualifications;
            if (roles.Contains("petSitter"))
            {
                yoe = _context.UserYearsOfExperience.AsEnumerable().First(x => x.UserId == user.Id)
                    .YearsOfExperience;
                qualifications = _context.QualificationsInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                    .Select(x => _context.Qualifications.AsEnumerable().First(y => y.Id == x.QualificationId))
                    .ToList();
                return Ok(new PersonalUserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = firstName,
                    LastName = lastName,
                    YearsOfExperience = yoe,
                    PhoneNumber = user.PhoneNumber,
                    ProfilePicUrl = ppu,
                    Qualifications = qualifications,
                    Rating = rating,
                    ServiceType = serviceType,
                    UserName = user.UserName,
                    UserType = userType,
                    Description = description,
                    Roles = roles.ToList()
                });
            }

            yoe = 0;
            qualifications = null;
            return Ok(new PersonalUserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = firstName,
                LastName = lastName,
                YearsOfExperience = yoe,
                PhoneNumber = user.PhoneNumber,
                ProfilePicUrl = ppu,
                Qualifications = qualifications,
                Rating = rating,
                ServiceType = serviceType,
                UserName = user.UserName,
                UserType = userType,
                Description = description,
                Roles = roles.ToList()
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser( string id )
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return BadRequest(new { Message = "User does not exist" });
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("petOwner"))
            {
                return Ok(new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == user.Id).FirstName,
                    LastName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == user.Id).LastName,
                    Qualifications = null,
                    YearsOfExperience = 0,
                    PhoneNumber = user.PhoneNumber,
                    ProfilePicUrl = _context.ProfilePicUsers.AsEnumerable().First(x => x.UserId == user.Id).ImageUrl,
                    Rating = _context.UserRatings.AsEnumerable().First(x => x.UserId == user.Id),
                    ServiceType = _context.ServiceTypeInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                        .Select(x => _context.ServiceTypes.AsEnumerable().First(y => y.Id == x.ServiceTypeId)).First(),
                    UserType = _context.UserTypesInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                        .Select(x => _context.UserTypes.AsEnumerable().First(y => y.Id == x.UserTypeId)).First().Name,
                    Description = _context.UserDescriptions.AsEnumerable().First(x => x.UserId == user.Id).Description,
                });
            }
            return Ok(new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == user.Id).FirstName,
                LastName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == user.Id).LastName,
                Qualifications = _context.QualificationsInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                    .Select(x => _context.Qualifications.First(y => y.Id == x.QualificationId)).ToList(),
                YearsOfExperience = _context.UserYearsOfExperience.AsEnumerable().First(x => x.UserId == user.Id).YearsOfExperience,
                PhoneNumber = user.PhoneNumber,
                ProfilePicUrl = _context.ProfilePicUsers.AsEnumerable().First(x => x.UserId == user.Id).ImageUrl,
                Rating = _context.UserRatings.AsEnumerable().First(x => x.UserId == user.Id),
                ServiceType = _context.ServiceTypeInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                    .Select(x => _context.ServiceTypes.AsEnumerable().First(y => y.Id == x.ServiceTypeId)).First(),
                UserType = _context.UserTypesInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                    .Select(x => _context.UserTypes.AsEnumerable().First(y => y.Id == x.UserTypeId)).First().Name,
                Description = _context.UserDescriptions.AsEnumerable().First(x => x.UserId == user.Id).Description,
            });
        }

        [HttpGet("AllPetOwners")]
        public async Task<ActionResult> GetAllPetOwners()
        {
            var users = _userManager.Users.ToList();
            if (users.Count < 1)
                return BadRequest();

            return Ok(users.AsEnumerable()
                .Where(x => _userManager.GetRolesAsync(x).Result.Contains("petOwner")).Select(user => new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == user.Id).FirstName,
                    LastName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == user.Id).LastName,
                    Qualifications = null,
                    YearsOfExperience = 0,
                    PhoneNumber = user.PhoneNumber,
                    ProfilePicUrl = _context.ProfilePicUsers.AsEnumerable().First(x => x.UserId == user.Id).ImageUrl,
                    Rating = _context.UserRatings.AsEnumerable().First(x => x.UserId == user.Id),
                    ServiceType = _context.ServiceTypeInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                        .Select(x => _context.ServiceTypes.AsEnumerable().First(y => y.Id == x.ServiceTypeId)).First(),
                    UserType = _context.UserTypesInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                        .Select(x => _context.UserTypes.AsEnumerable().First(y => y.Id == x.UserTypeId)).First().Name,
                    Description = _context.UserDescriptions.AsEnumerable().First(x => x.UserId == user.Id).Description,
                }).ToList());
        }


        [HttpGet("AllPetSitters")]
        public async Task<ActionResult> GetAllPetSitters()
        {
            var users = _userManager.Users.ToList();

            if (users.Count < 1)
                return BadRequest();

            return Ok(users.AsEnumerable()
                .Where(x => _userManager.GetRolesAsync(x).Result.Contains("petSitter")).Select(user => new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == user.Id).FirstName,
                    LastName = _context.UserFullNames.AsEnumerable().First(x => x.UserId == user.Id).LastName,
                    Qualifications = _context.QualificationsInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                        .Select(x => _context.Qualifications.First(y => y.Id == x.QualificationId)).ToList(),
                    YearsOfExperience = _context.UserYearsOfExperience.AsEnumerable().First(x => x.UserId == user.Id).YearsOfExperience,
                    PhoneNumber = user.PhoneNumber,
                    ProfilePicUrl = _context.ProfilePicUsers.AsEnumerable().First(x => x.UserId == user.Id).ImageUrl,
                    Rating = _context.UserRatings.AsEnumerable().First(x => x.UserId == user.Id),
                    ServiceType = _context.ServiceTypeInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                        .Select(x => _context.ServiceTypes.AsEnumerable().First(y => y.Id == x.ServiceTypeId)).First(),
                    UserType = _context.UserTypesInUsers.AsEnumerable().Where(x => x.UserId == user.Id)
                        .Select(x => _context.UserTypes.AsEnumerable().First(y => y.Id == x.UserTypeId)).First().Name,
                    Description = _context.UserDescriptions.AsEnumerable().First(x => x.UserId == user.Id).Description,
                }).ToList());
        }

        [HttpPost("AddNewUser")]
        public async Task<ActionResult> AddNewUser( Register model )
        {
            var resultByEmail = await _userManager.FindByEmailAsync(model.Email);

            var resultByUsername = await _userManager.FindByNameAsync(model.UserName);

            if (resultByEmail != null || resultByUsername != null || model.Password != model.ConfirmPassword)
                return Conflict();

            var user = await _userManager.CreateAsync(new IdentityUser
            {
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
            }, model.Password);

            await _context.SaveChangesAsync();
            var userId = await _userManager.FindByNameAsync(model.UserName);
            await _context.UserFullNames.AddAsync(new UserFullName
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserId = userId.Id
            });

            await _context.SaveChangesAsync();

            return CreatedAtAction("AddNewUser", new { success = user.Succeeded },
                await _userManager.FindByNameAsync(model.UserName));
        }


        [HttpPost("AddUserRole")]
        public async Task<ActionResult> AddUserRole( UserDetailsDto userDetails )
        {
            if (User.Identity.Name == null)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null || userDetails.Role == "")
                return BadRequest();

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains(userDetails.Role))
            {
                _context.QualificationsInUsers.AsEnumerable().Where(x => x.UserId == user.Id).ToList().ForEach(x =>
                {
                    _context.QualificationsInUsers.Remove(x);
                });
            }

            await _userManager.AddToRoleAsync(user, userDetails.Role);
            if (userDetails.Role == "petSitter")
            {
                    userDetails.QualificationIds.ForEach(x =>
                {
                    _context.QualificationsInUsers.Add(new QualificationInUser
                    {
                        QualificationId = x,
                        UserId = user.Id
                    });
                });
                    
                    await _context.UserYearsOfExperience.AddAsync(new UserYearsOfExperience()
                    {
                        UserId = user.Id,
                        YearsOfExperience = userDetails.YearsOfExperience
                    });
            }

            await _context.UserTypesInUsers.AddAsync(new UserTypeInUser()
            {
                UserId = user.Id,
                UserTypeId = _context.UserTypes.First(x => x.Name == userDetails.Role).Id
            });
            _context.ServiceTypeInUsers.Add(new ServiceTypeInUser
            {
                ServiceTypeId = userDetails.ServiceTypeId,
                UserId = user.Id
            });
            if (userDetails.description != null)
            {
                await _context.UserDescriptions.AddAsync(new UserDescription()
                {
                    Description = userDetails.description,
                    UserId = user.Id
                });
            }
            else
            {
                await _context.UserDescriptions.AddAsync(new UserDescription()
                {
                    Description = "",
                    UserId = user.Id
                });
            }

            _context.UserRatings.Add(new UserRating()
            {
                Rating = 0,
                UserId = user.Id
            });

            _context.ProfilePicUsers.Add(new ProfilePicUser()
            {
                ImageUrl = userDetails.Gender == "male"
                    ? "https://i.ibb.co/0JGWD44/user-male.png"
                    : "https://i.ibb.co/Jkb7FzG/user-female.png",
                UserId = user.Id
            });

            await _context.SaveChangesAsync();

            return CreatedAtAction("AddUserRole", new { id = user.Id }, user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginUser( Login model )
        {
            if (!ModelState.IsValid)
                return BadRequest("Credentials invalid");

            if (model.Username == string.Empty)
                return BadRequest("Invalid Username");

            if (model.Password == string.Empty)
                return BadRequest("Invalid Password");

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
                return BadRequest("User Does Not Exist");

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return BadRequest("Password is not correct");

            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            var tokenHandler = new JwtSecurityTokenHandler();

            Response.Cookies.Append("Token", tokenHandler.WriteToken(token), new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true });
            return Ok("User Logged In");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAccount()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null) return BadRequest();

            _context.QualificationsInUsers.RemoveRange(_context.QualificationsInUsers.AsEnumerable().Where(x => x.UserId == user.Id));
            _context.UserFullNames.Remove(_context.UserFullNames.AsEnumerable().First(x => x.UserId == user.Id));
            _context.UserDescriptions.Remove(_context.UserDescriptions.AsEnumerable().First(x => x.UserId == user.Id));
            _context.ProfilePicUsers.Remove(_context.ProfilePicUsers.AsEnumerable().First(x => x.UserId == user.Id));
            _context.ServiceTypeInUsers.Remove(_context.ServiceTypeInUsers.AsEnumerable().First(x=> x.UserId == user.Id));
            _context.UserYearsOfExperience.Remove(_context.UserYearsOfExperience.AsEnumerable()
                .First(x => x.UserId == user.Id));
            _context.UserTypesInUsers.Remove(_context.UserTypesInUsers.AsEnumerable().First(x => x.UserId == user.Id));
            await _context.SaveChangesAsync();
            return Ok("User Deleted");
        }

        [HttpGet("Logout")]
        public async Task<ActionResult> Logout()
        {
            Response.Cookies.Delete("Token", new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true });
            return Ok("user logged out");
        }
    }
}