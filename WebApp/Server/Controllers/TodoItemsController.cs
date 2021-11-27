
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Server.Data;
using WebApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.Server.Models;
using Microsoft.Extensions.Logging;

namespace WebApp.Server.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    

    public class TodoItemsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<TodoItemsController> logger;
        public List<Ticket> TicketItems { get; set; } = new List<Ticket>()
        {
            new Ticket { Title = "number 11:",  isDone = false},
            new Ticket { Title = "number 21:",  isDone = false}
        };

        public TodoItemsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            ILogger<TodoItemsController> logger)
        {
            this.context = context;
            this.userManager = userManager;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<Ticket> SingleTodoItem(Guid id)
        {
            var ticket = context.Tickets.FirstOrDefault(p => p.Id == id);

            if (ticket == null)
            {
                return NotFound("This post does not exist / could not find when getting from controller");
            }

            return Ok(ticket);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> Get() => context.Tickets;

        [HttpPost]
        public async Task <ActionResult> CreateNewTodoItem(Ticket item)
        {
            ApplicationUser userValid;
            try
            {
                userValid = userManager.Users.Where(s => s.Email == item.owningUser.Username).First();
                var ID = userManager.Users.First(c => c.Email.Contains(userValid.Email)).Id;

                item.owningUser = new Shared.User();
                item.owningUser.Username = userValid.Email;
                item.owningUser.UserId = new Guid();
                item.owningUser.Password = "random";

                context.Tickets.Add(item);
                context.Users.Add(item.owningUser);

                await context.SaveChangesAsync();
                return NoContent();

            } catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Ticket>> Update(Ticket item)
        {
            context.Tickets.Update(item);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Ticket>> Delete(Guid id)
        {
            var todoItem = context.Tickets.FirstOrDefault(c => c.Id == id);
            context.Tickets.Remove(todoItem);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}