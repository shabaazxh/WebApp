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
        public Guid CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }
    }
}
