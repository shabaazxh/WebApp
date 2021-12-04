using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Shared;

namespace WebApp.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public ICollection<Project> projects { get; set; }

        public ICollection<Ticket> assignedTickets { get; set; }

    }
}
