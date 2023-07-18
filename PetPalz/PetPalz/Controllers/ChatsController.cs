using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPalz.Data;
using PetPalz.Models;
using PetPalz.Models.Dtos;

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
            return Ok(_context.Chats.Where(x => x.UserId1 == user.Id).Select(x => new ChatDto
            {
                Id = x.Id,
                 UserId1 = x.UserId1,
                 UserId2 = x.UserId2,
                 Messages = _context.ChatMessages.Where(y => y.ChatId == x.Id).OrderByDescending(y => y.DeliverDateTime).ToList()
            }));
        }

        [HttpPost]
        public async Task<ActionResult> UserChats(ChatDto chat)
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
            return Ok();
        }

    }

}
