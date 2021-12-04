using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Shared;

namespace WebApp.Shared
{
    public class ApplicationUser : IdentityUser
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public DateTime LastAccessed { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
