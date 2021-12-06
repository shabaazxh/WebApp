using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Server.Data;
using WebApp.Shared;

namespace WebApp.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Tickets // GET ALL TICKETS THAT MATCH USERID -- Tickets assigned to current user
        [HttpGet("{currentUserID}/UserTickets")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets(string currentUserID)
        {
            try
            {
                var query = _context.Tickets.Include(u => u.CreatedBy).Where(t => t.UserID.ToString().Equals(currentUserID));
                return await query.ToListAsync();
            } catch (Exception e)
            {
                return new List<Ticket>();
            }

        }

        // Tickets that have been created by the user
        [HttpGet("{currentUserID}/User/CreatedTickets")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetUserCreatedTickets(string currentUserID)
        {
            try
            {
                var query = _context.Tickets.Include(u => u.AssignedUser).Include(u => u.CreatedBy).Where(t => t.CreatedByUser.ToString().Equals(currentUserID));
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                return new List<Ticket>();
            }

        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(Guid id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tickets :::: CREATE A TICKET ::::
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{createdByUserID}")]
        public async Task<ActionResult<Ticket>> PostTicket(string createdByUserID, Ticket ticket)
        {
            ApplicationUser userValid;
            try
            {
                var createdByUser = _userManager.Users.First(u => u.Id.Equals(createdByUserID));
               
                // Find user the ticket is being assigned to
                userValid = _userManager.Users.Where(s => s.Email == ticket.AssignedUser.UserName).First();
                var ID = _userManager.Users.First(c => c.Email.Equals(userValid.Email)).Id;

                ticket.AssignedUser = userValid;
                ticket.UserID = Guid.Parse(ID);
                ticket.CreatedByUser = Guid.Parse(createdByUser.Id);
                ticket.CreatedBy = createdByUser;

                _context.Tickets.Add(ticket);
                 await _context.SaveChangesAsync();

                return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);

            } catch (InvalidOperationException)
            {
                return BadRequest();
            }

        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(Guid id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }

    }
}
