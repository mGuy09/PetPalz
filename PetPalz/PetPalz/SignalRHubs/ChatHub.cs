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
        
       
    }
}
