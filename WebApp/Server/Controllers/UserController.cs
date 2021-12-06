using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Server.Data;
using WebApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Server.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            _signInManager = signInManager;
        }

        // GET currently logged in user
        [HttpGet("{findUserID}")]
        public async Task<ActionResult<ApplicationUser>> GetCurrentUser(string findUserID)
        {
            var user = userManager.Users.First(x => x.Id.Equals(findUserID));
            user.LastAccessed = DateTime.Now;
            context.SaveChanges();

            return user;
        }

        // Get currently logged in users tickets
        [HttpGet("{userID}/UsersTickets")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetUserTickets(string userID)
        {
            try { 
                var findTickets = await context.Tickets.Where(t => t.UserID.ToString().Equals(userID)).ToListAsync();
                return findTickets;
            } catch(Exception)
            {
                return new List<Ticket>();
            }
            
        }

        // Name of the user the ticket belongs to
        [HttpGet("{ticketID}/UserForTicket")]
        public async Task<ActionResult<ApplicationUser>> GetUserForTicket(Guid ticketID)
        {
            var ticketUser = context.Tickets.First(p => p.Id == ticketID);
            var user = userManager.Users.First(x => x.Id.Equals(ticketUser.UserID.ToString()));

            return new ApplicationUser { Id = user.Id, UserName = user.UserName };
        }

        [HttpPut("delete/user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = userManager.Users.Include( u => u.Projects).FirstOrDefault(u => u.Id.Equals(id));
            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return LocalRedirect("/");
            } else
            {
                return BadRequest();
            }
        }

    }
}