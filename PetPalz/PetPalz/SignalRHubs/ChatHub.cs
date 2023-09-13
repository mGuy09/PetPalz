using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using PetPalz.Data;

namespace PetPalz.SignalRHubs
{
    public class ChatHub: Hub
    {
        private readonly PetPalzContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ChatHub(UserManager<IdentityUser> userManager, PetPalzContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        
        public async Task CreateChat(string userId1, string userId2)
        {
            var chatId = _context.Chats.First(x => x.UserId1 == userId1 && x.UserId2 == userId2).Id;
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());

        }
        public async Task SendMessage(string message, string userId1, string userId2)
        {
            var chatId = _context.Chats.First(x => x.UserId1 == userId1 && x.UserId2 == userId2).Id;
            var user = await _userManager.FindByIdAsync(userId1);
            await Clients.Group(chatId.ToString()).SendAsync("RecieveMessage", user, DateTime.Now);
        }
    }
}
