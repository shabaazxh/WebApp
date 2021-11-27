using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApp.Shared
{
    public class Company
    {
        [Key]
        public Guid CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public DateTime JoinDate { get; set; }

        public List<Project> WorkingOnProject { get; set; }
    }
}
