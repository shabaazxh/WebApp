using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Shared
{
    public class User
    {
        public Guid UserId { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        public string Password { get; set; }

        public List<Project> projects { get; set; }

        public List<Ticket> assignedTickets { get; set; }

    }
}
