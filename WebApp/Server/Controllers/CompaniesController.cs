using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Server.Data;
using WebApp.Shared;

namespace WebApp.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Companies: Returns the company and the projects the company is working on
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            var companies = _context.Companies.ToList();

            // for each company find its projects
            foreach (var company in companies)
            {
                var query = _context.Projects.Where(p => p.companyID.ToString().Equals(company.CompanyId.ToString())); // match project company id to current company id
                List<Project> companyProjects = new List<Project>();

                // --- This is done to prevent the JSON cylic error --- 
                // loop through the projects from query, extract information and add to list
                foreach (var project in query.ToList())
                {
                    Project x = new Project { ProjectId = project.ProjectId, ProjectName = project.ProjectName, ProjectDescription = project.ProjectDescription };
                    companyProjects.Add(x);
                }

                company.WorkingOnProject = companyProjects;
            }

            return companies;
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(Guid id, Company company)
        {
            if (id != company.CompanyId)
            {
                return BadRequest();
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.CompanyId }, company);
        }

        // Find company and add it to a project 
        [HttpPost("{companyName}")]
        public async Task<ActionResult<Company>> AddCompanyToProject(string companyName)
        {
            try
            {
                // find company
                Company foundCompany = await _context.Companies.Where(c => c.CompanyName.Equals(companyName)).FirstAsync();

                // return company if found
                return foundCompany;
            }
            catch (InvalidOperationException)
            {
                // if not found, return bad request 
                return BadRequest();
            }
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(Guid id)
        {
            return _context.Companies.Any(e => e.CompanyId == id);
        }
    }
}
