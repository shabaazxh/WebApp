using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Server.Data;
using WebApp.Shared;

namespace WebApp.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PredictionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<Dictionary<string, int>>> GetProjects(string date)
        {

            DateTime given_date = DateTime.Parse(date);

            var companies = await _context.Companies.ToListAsync();
            List<Project> foundProjects = new List<Project>(); 
            int total_projects_per_company = 0;
            int users_per_project_sum = 0;

            Dictionary<string, int> d = new Dictionary<string, int>();

            for(int i = 0; i < companies.Count(); i++)
            {
                var query = _context.Projects.Where(p => p.isComplete == true) //project is complete
                    .Where(p => p.EndDate.Date < given_date.Date) //completion date is before future date given
                    .Where(p => p.companyID.ToString().Equals(companies[i].CompanyId.ToString()));
                
                foreach(var proj in query)
                {
                    foundProjects.Add(proj);
                }

                var count = query.Count();
                total_projects_per_company += count;
            }

            foreach (var project in foundProjects)
            {
                var query = _context.Users.Where(u => u.Projects.Any(p => p.ProjectId.ToString().Equals(project.ProjectId.ToString()))); //find all users for the project
                var userCount = query.Count();
                users_per_project_sum += userCount;
            }

/*            if(companies.Count < 5 || foundProjects.Count < 5)
            {
                return BadRequest();
            }*/

            // calculate average for both results
            total_projects_per_company = total_projects_per_company / companies.Count();
            users_per_project_sum = users_per_project_sum / foundProjects.Count();


            d.Add("users", users_per_project_sum);
            d.Add("projects", total_projects_per_company);

            return d;
        }

    }
}
