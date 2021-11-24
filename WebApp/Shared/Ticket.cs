using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApp.Shared
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool isDone { get; set; }
    }
}
