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

        [Required]
        public string ProjectName { get; set; } 

        public string ProjectDescription { get; set; }

        public List<User> Users { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Company assignedCompanyForProject { get; set; }
    }
}
