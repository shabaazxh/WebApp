
using WebApp.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace WebApp.Client.Services
{
    interface ITodoItemService
    {
        Task<List<Ticket>> GetAllItems();
        Task<Ticket> GetSingleTodoItem(Guid id);
        Task<Ticket> CreateNewToDoItem(Ticket item);
        Task<Ticket> UpdateToDoItem(Guid id, Ticket item);

        Task<Ticket> DeleteToDoItem(Guid id);

    }
}
