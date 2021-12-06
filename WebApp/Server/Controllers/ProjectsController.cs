using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.Server.Data;
using WebApp.Shared;

namespace WebApp.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Projects // GET ALL PROJECTS
        [HttpGet("{userID}/UserProjects")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects(string userID)
        {
            var projects = await _context.Projects.Where(p => p.AssignedUsersToProject.Any(u => u.Id.Equals(userID))).ToListAsync(); //find projects only for the currently logged in user

            foreach (var project in projects)
            {
                var query = _context.Users.Where(u => u.Projects.Any(p => p.ProjectId.ToString().Equals(project.ProjectId.ToString()))); //find all users for the project

                project.AssignedUsersToProject = query.ToList(); //set found users to the assigned users list for the project
            }

            return projects;
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            var query = _context.Users.Where(u => u.Projects.Any(p => p.ProjectId.ToString().Equals(project.ProjectId.ToString()))); //find all users for the project
            project.AssignedUsersToProject = query.ToList();

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(Guid id, Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest();
            }
            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // Remove user from a project
        [HttpPut("removeUser/{username}/{projectID}")]
        public async Task<IActionResult> RemoveUserFromProject(string username, Guid projectID)
        {
            var project = await _context.Projects.Include(u => u.AssignedUsersToProject).FirstOrDefaultAsync(p => p.ProjectId.ToString().Equals(projectID.ToString()));

            project.AssignedUsersToProject.Remove(project.AssignedUsersToProject.First(u => u.UserName == username));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(projectID))
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

        // Add user to the project
        [HttpPut("add/User/{userName}/{projID}")]
        public async Task<IActionResult> AddUserToProject(string userName, Guid projID)
        {

            var project = _context.Projects.Include(u => u.AssignedUsersToProject).FirstOrDefault(p => p.ProjectId.ToString().Equals(projID.ToString()));

            try
            {
                var user = _context.Users.First(u => u.UserName.Equals(userName));

                if (!project.AssignedUsersToProject.Any(u => u.UserName.Equals(user.UserName))) { 
                    project.AssignedUsersToProject.Add(user);                    
                } else
                {
                    return Forbid();
                }
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(projID))
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



        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {

            foreach (var x in project.AssignedUsersToProject)
            {
                _context.Entry(x).State = EntityState.Unchanged;
            }

            project.companyID = project.assignedCompanyForProject.CompanyId;

            _context.Entry(project.assignedCompanyForProject).State = EntityState.Unchanged;

            _context.Projects.Add(project);
            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
                // return CreatedAtAction("GetProject", new { id = project.ProjectId }, project);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Find and return user to add to the project
        [HttpPost("{userEmail}")]
        public async Task<ActionResult<ApplicationUser>> AddUserToProject(string userEmail)
        {
            try
            {
                // Find user
                ApplicationUser userValid = await _userManager.Users.Where
                    (s => s.Email == userEmail).FirstAsync();

                return userValid;
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(Guid id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }


        [HttpGet("{projectID}/CompanyProject")]
        public async Task<ActionResult<Company>> GetCompanyForProject(Guid projectID)
        {
            var ticketUser = _context.Projects.First(p => p.ProjectId.ToString().Equals(projectID.ToString()));

            var company = _context.Companies.First(x => x.CompanyId.ToString().Equals(ticketUser.companyID.ToString()));

            return new Company { CompanyId = company.CompanyId, CompanyName = company.CompanyName };
        }
    }
}
