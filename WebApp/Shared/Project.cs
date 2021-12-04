using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApp.Shared
{
    public class Project
    {
        [Key]
        public Guid ProjectId { get; set; }

        public Guid companyID { get; set; }

        [Required]
        public string ProjectName { get; set; } 

        public string ProjectDescription { get; set; }

        public bool isComplete { get; set; } = false;

        [Range(0,100)]
        public int currentProgress { get; set; } = 0;

        public ICollection<ApplicationUser> AssignedUsersToProject { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Company assignedCompanyForProject { get; set; }
    }
}
