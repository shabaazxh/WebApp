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
            var user = userManager.Users.First(x => x.Id.Equals(findUserID)); //find user
            user.LastAccessed = DateTime.Now; //set users last login to now
            context.SaveChanges(); // save to database

            return user;
        }

        // Name of the user the ticket belongs to
        [HttpGet("{ticketID}/UserForTicket")]
        public async Task<ActionResult<ApplicationUser>> GetUserForTicket(Guid ticketID)
        {
            var ticketUser = context.Tickets.First(p => p.Id == ticketID);
            var user = userManager.Users.First(x => x.Id.Equals(ticketUser.UserID.ToString()));

            return new ApplicationUser { Id = user.Id, UserName = user.UserName };
        }

    }
}