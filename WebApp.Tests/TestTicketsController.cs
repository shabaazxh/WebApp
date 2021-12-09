using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Server.Controllers;
using WebApp.Server.Data;
using WebApp.Shared;
using Xunit;

namespace WebApp.Tests
{
    public class TestProjectsController
    {
        [Fact]
        public async Task When_create_ticket_item_with_invalid_current_user()
        {
            var user = new ApplicationUser
            {
                Id = new Guid().ToString(),
                UserName = "test@gmail.com",
                Email = "test@gmaill.com"
            };

            var ticket = new Ticket
            {
                Title = "testTicket",
                CreatedByUser = Guid.Parse(user.Id),
                UserID = Guid.Parse(user.Id),
                AssignedUser = user,
                CreatedBy = user
            };

            var controller = new TicketsController(null, null);

            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await controller.PostTicket(user.Id, ticket)
                );

        }


        [Fact]
        public async Task When_delete_ticket_item_with_invalid_ticket_id()
        {
            var user = new ApplicationUser
            {
                Id = "5685a830-cb82-4b60-b459-c0852cc74563",
                UserName = "admin@gmail.com",
                Email = "admin@gmaill.com"
            };

            var project = new Project();

            var ticket = new Ticket
            {
                Id = new Guid(),
                Title = "testTicket",
                CreatedByUser = Guid.Parse(user.Id),
                UserID = Guid.Parse(user.Id),
                AssignedUser = user,
                CreatedBy = user,
                AssociatedProject = project,
                ticketType = TicketType.BugTicket,
                isDone = true,
                project_id = project.ProjectId,
                Date = DateTime.Now
            };

            var controller = new TicketsController(null, null);

            Assert.ThrowsAsync<NullReferenceException>(
            async () => await controller.DeleteTicket(ticket.Id)
            );

        }
    }
}
