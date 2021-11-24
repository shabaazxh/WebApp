using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApp.Shared
{
    public class Project
    {
        public Guid ProjectId { get; set; }
        [Required]
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
    }
}
