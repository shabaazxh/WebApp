using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [Fact]
        public void When_retrieving_single_ticket_returns_correct_ticket()
        {
            var user = new ApplicationUser
            {
                Id = "5685a830-cb82-4b60-b459-c0852cc74563",
                UserName = "admin@gmail.com",
                Email = "admin@gmaill.com"
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "WebAppDatabase")
                .Options;

            var someOptions = Options.Create(new OperationalStoreOptions());

            using(var context = new ApplicationDbContext(options, someOptions))
            {
                context.Tickets.Add(new Ticket {Id = new Guid(), Title="random", AssignedUser = user, UserID = Guid.Parse(user.Id)});
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.SaveChanges();
            }

            using(var context = new ApplicationDbContext(options, someOptions))
            {
                List<Ticket> tickets = new List<Ticket>();
                tickets = context.Tickets.ToList();
                var controller = new TicketsController(context, null);
                var result = controller.GetTicket(tickets.ElementAt(0).Id);

                Assert.True(result.Result.Value.Id == tickets.ElementAt(0).Id);
                
            }
        }

        [Fact]
        public void When_retrieving_single_ticket_fails_ticket_does_not_exist()
        {
            var user = new ApplicationUser
            {
                Id = "5685a830-cb82-4b60-b459-c0852cc74563",
                UserName = "admin@gmail.com",
                Email = "admin@gmaill.com"
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "WebAppDatabase")
                .Options;

            var someOptions = Options.Create(new OperationalStoreOptions());

            using (var context = new ApplicationDbContext(options, someOptions))
            {
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.SaveChanges();
            }

            var invalid_ticket = new Ticket { Id = new Guid(), Title = "odd", AssignedUser = user, UserID = Guid.Parse(user.Id) };
            using (var context = new ApplicationDbContext(options, someOptions))
            {
                List<Ticket> tickets = new List<Ticket>();
                tickets = context.Tickets.ToList();
                var controller = new TicketsController(context, null);
                var result = controller.GetTicket(invalid_ticket.Id);

                Assert.ThrowsAsync<InvalidOperationException>(
                async () => await controller.GetTicket(invalid_ticket.Id)
                );
            }
        }

        [Fact]
        public void When_updating_valid_ticket()
        {
            var user = new ApplicationUser
            {
                Id = "5685a830-cb82-4b60-b459-c0852cc74563",
                UserName = "admin@gmail.com",
                Email = "admin@gmaill.com"
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "WebAppDatabase")
                .Options;

            var someOptions = Options.Create(new OperationalStoreOptions());

            using (var context = new ApplicationDbContext(options, someOptions))
            {
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.SaveChanges();
            }

            var invalid_ticket = new Ticket { Id = new Guid(), Title = "odd", AssignedUser = user, UserID = Guid.Parse(user.Id) };
            using (var context = new ApplicationDbContext(options, someOptions))
            {
                List<Ticket> tickets = new List<Ticket>();
                tickets = context.Tickets.ToList();
                var controller = new TicketsController(context, null);
                var result = controller.PutTicket(tickets.ElementAt(0).Id, tickets.ElementAt(0));

                Assert.IsType<NoContentResult>(result.Result);
            }
        }

        [Fact]
        public void When_updating_ticket_where_ticket_id_is_mismatch()
        {
            var user = new ApplicationUser
            {
                Id = "5685a830-cb82-4b60-b459-c0852cc74563",
                UserName = "admin@gmail.com",
                Email = "admin@gmaill.com"
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "WebAppDatabase")
                .Options;

            var someOptions = Options.Create(new OperationalStoreOptions());

            using (var context = new ApplicationDbContext(options, someOptions))
            {
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.SaveChanges();
            }

            var invalid_ticket = new Ticket { Id = new Guid(), Title = "odd", AssignedUser = user, UserID = Guid.Parse(user.Id) };
            using (var context = new ApplicationDbContext(options, someOptions))
            {
                List<Ticket> tickets = new List<Ticket>();
                tickets = context.Tickets.ToList();
                var controller = new TicketsController(context, null);
                var result = controller.PutTicket(tickets.ElementAt(0).Id, invalid_ticket);

                Assert.IsType<BadRequestResult>(result.Result);
            }
        }


        [Fact]
        public void When_updating_ticket_where_ticket_is_invalid()
        {
            var user = new ApplicationUser
            {
                Id = "5685a830-cb82-4b60-b459-c0852cc74563",
                UserName = "admin@gmail.com",
                Email = "admin@gmaill.com"
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "WebAppDatabase")
                .Options;

            var someOptions = Options.Create(new OperationalStoreOptions());

            using (var context = new ApplicationDbContext(options, someOptions))
            {
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.Tickets.Add(new Ticket { Id = new Guid(), Title = "random", AssignedUser = user, UserID = Guid.Parse(user.Id) });
                context.SaveChanges();
            }

            var invalid_ticket = new Ticket { Id = new Guid(), Title = "odd", AssignedUser = user, UserID = Guid.Parse(user.Id) };
            using (var context = new ApplicationDbContext(options, someOptions))
            {
                List<Ticket> tickets = new List<Ticket>();
                tickets = context.Tickets.ToList();
                var controller = new TicketsController(context, null);
                var result = controller.PutTicket(invalid_ticket.Id, invalid_ticket);

                Assert.IsType<NotFoundResult>(result.Result);
            }
        }
    }
}
