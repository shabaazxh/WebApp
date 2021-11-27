using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApp.Shared
{
    public enum TicketType { 
    
        TodoTicket,
        BugTicket,
        SupportTicket
    }
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        public bool isDone { get; set; }

        public User owningUser { get; set; }

        public TicketType ticketType { get; set; }

        public DateTime Date { get; set; }
    }
}
