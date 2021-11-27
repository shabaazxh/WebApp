using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Server.Data;
using WebApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.Server.Models;
using System;
using System.Data.Entity;

namespace WebApp.Server.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            List<User> giveback = new List<User>();
            List<Project> projects = new List<Project>();
            
            var result = userManager.Users.ToList();

            for(int i = 0; i < result.Count(); i++)
            {
                Project pro = new Project();
                Ticket tick = new Ticket();
                tick.Title = "Ticket " + i * 2;
                tick.ticketType = TicketType.SupportTicket;
                pro.ProjectName = "project: " + i;

                User x = new User();
                x.Username = result[i].Email;
                x.projects = new List<Project>();
                x.projects.Add(pro);
                x.assignedTickets = new List<Ticket>();
                
                x.assignedTickets.Add(tick);

                giveback.Add(x);
            }

            return giveback;
        }

        [HttpGet("{ticketID}")]
        public async Task<ActionResult<User>> GetUserForTicket(Guid ticketID)
        {
            var ticketUser = await context.Tickets.FindAsync(ticketID);

            if(ticketUser.owningUser != null)
                return ticketUser.owningUser;
            else
                return BadRequest("no owning user");
        }

    }
}