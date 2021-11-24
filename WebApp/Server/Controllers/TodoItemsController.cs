
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

namespace WebApp.Server.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    

    public class TodoItemsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Models.ApplicationUser> userManager;
        public List<Ticket> TicketItems { get; set; } = new List<Ticket>()
        {
            new Ticket { Title = "number 11:",  isDone = false},
            new Ticket { Title = "number 21:",  isDone = false}
        };

        public TodoItemsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<Ticket> SingleTodoItem(Guid id)
        {
            Console.WriteLine("CONTROLLER: " + id);

            var content = context.Tickets.Find(id);
            var post = context.Tickets.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound("This post does not exist / could not find when getting from controller");
            }

            return Ok(post);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> Get()
        {
            return context.Tickets;
        }

        [HttpPost]
        public async Task <ActionResult<Ticket>> CreateNewTodoItem(Ticket item)
        {
            context.Tickets.Add(item);
            await context.SaveChangesAsync();

            return item;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Ticket>> Update(Guid id,Ticket item)
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