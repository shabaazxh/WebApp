using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Shared;

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

        public Guid project_id { get; set; }

        public Guid UserID { get; set; } //who the ticket is assigned to

        public Guid CreatedByUser { get; set; } //who this ticket is created by

        public ApplicationUser CreatedBy { get; set; }
        
        public ApplicationUser AssignedUser { get; set; }

        public Project AssociatedProject { get; set; }

        public TicketType ticketType { get; set; }

        public DateTime Date { get; set; }
    }
}
