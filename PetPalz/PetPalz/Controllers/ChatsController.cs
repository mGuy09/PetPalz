using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetPalz.Data;
using PetPalz.Models;

namespace PetPalz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly PetPalzContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ChatsController(PetPalzContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> UserChats()
        {
            
            if (User.Identity.Name is null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return BadRequest();
            }
            var chats = _context.Chats.Where(x => x.UserId1 == user.Id || x.UserId2 == user.Id).Select(x => new Chat
            {
                Id = x.Id,
                UserId1 = x.UserId1,
                UserId2 = x.UserId2,
            }).ToList();
            return Ok(chats);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetChatbyChat (string userId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var chat = _context.Chats.Where(x => x.UserId1 == user.Id && x.UserId2 == userId);
            if(chat is null)
            {
                return BadRequest();
            }
            return Ok(chat);
        }

        [HttpPost]
        public async Task<ActionResult> UserChats(Chat chat)
        {
            if (chat == null)
            {
                return BadRequest();
            }

            await _context.Chats.AddAsync(new Chat
            {
                UserId1 = chat.UserId1,
                UserId2 = chat.UserId2
            });
            await _context.SaveChangesAsync();

            var userId2 = _context.Chats.FirstOrDefault(x => x.UserId1 == chat.UserId1 && x.UserId2 == chat.UserId2).UserId2;
            return Ok(userId2);
        }

    }

}
