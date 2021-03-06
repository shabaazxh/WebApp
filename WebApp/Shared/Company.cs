using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public ICollection<Project> WorkingOnProject { get; set; }
    }
}
