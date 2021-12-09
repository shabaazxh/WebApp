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
using Microsoft.AspNetCore.Authorization;

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

        // Find all tickets associated to current project
        [HttpGet("projects/tickets/{projectID}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsForProject(Guid projectID)
        {
            try
            {
                var query = _context.Tickets.Include(t => t.AssignedUser).Include(t => t.CreatedBy).Where(u => u.project_id.ToString().Equals(projectID.ToString())); //find all tickets for current project
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                return new List<Ticket>();
            }

        }

        // GET: api/Tickets // GET ALL TICKETS THAT MATCH USERID -- Tickets assigned to current user
        [HttpGet("{currentUserID}/UserTickets")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets(string currentUserID)
        {
            try
            {
                var query = _context.Tickets.Include(u => u.CreatedBy).Include(u => u.AssignedUser).Include(p => p.AssociatedProject).Where(t => t.UserID.ToString().Equals(currentUserID));
                //var query = _context.Tickets.Include(u => u.CreatedBy).Include(u => u.AssignedUser).Where(t => t.UserID.ToString().Equals(currentUserID));
                return await query.ToListAsync();
            } catch (Exception e)
            {
                return new List<Ticket>();
            }

        }

        // Tickets that have been CREATED by the user
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

        // GET A TICKET
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(Guid id)
        {
            var ticket = await _context.Tickets.Include(t => t.AssignedUser).Where(t => t.Id.ToString().Equals(id.ToString())).FirstAsync();

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        [HttpPut("changeuser/{username}/{ticketID}")]
        public async Task<IActionResult> ChangeTicketUser(string username, Guid ticketID)
        {
            ApplicationUser findUser;
            Ticket ticketItem;
            try {
                findUser = _userManager.Users.First(u => u.Email.Equals(username));
                
            } catch (Exception)
            {
                return Forbid(); //user not found
            }

            try
            {
                ticketItem = _context.Tickets.Where(t => t.Id.ToString().Equals(ticketID.ToString())).First();
            } catch (Exception)
            {
                return BadRequest(); //ticket not found
            }


            ticketItem.AssignedUser = findUser;
            ticketItem.UserID = Guid.Parse(findUser.Id);

            //_context.Tickets.Update(ticketItem);

            await _context.SaveChangesAsync();

            return NoContent();

        }

        // EDIT A TICKET
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(Guid id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;
            _context.Entry(ticket).Property(p => p.UserID).IsModified = false;

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
            Project findProject;
            try
            {
                findProject = await _context.Projects.Where(p => p.ProjectName.Equals(ticket.AssociatedProject.ProjectName)).FirstAsync();
            
            } catch (InvalidOperationException)
            {
                return Forbid(); // if no project could be found with the given name
            }

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
                ticket.AssociatedProject = findProject;
                ticket.project_id = findProject.ProjectId;

                //_context.Entry(ticket.AssociatedProject).State = EntityState.Unchanged;

                _context.Tickets.Add(ticket);
                 await _context.SaveChangesAsync();

                return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);

            } catch (InvalidOperationException)
            {
                return NotFound();
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
