using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using WebApp.Shared;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebApp.Client.Services
{
    public class TodoItemService : ITodoItemService
    {

        private readonly HttpClient _http;

        public TodoItemService(HttpClient http)
        {
            _http = http;
            
        }

        public async Task<Ticket> CreateNewToDoItem(Ticket item)
        {
            var result = await _http.PostAsJsonAsync("TodoItems", item);
            return await result.Content.ReadFromJsonAsync<Ticket>();
        }

        public async Task<Ticket> DeleteToDoItem(Guid id)
        {
            Console.WriteLine("GOT PASSED: " + id);
            HttpResponseMessage response = await _http.DeleteAsync($"TodoItems/{id}");
            return response.Content.ReadFromJsonAsync<Ticket>().Result;
        }

        public async Task<List<Ticket>> GetAllItems()
        {
            return await _http.GetFromJsonAsync<List<Ticket>>("TodoItems");
        }

        public async Task<Ticket> GetSingleTodoItem(Guid id)
        {
            var result = await _http.GetFromJsonAsync<Ticket>($"TodoItems/{id}");
            return result;
           /* if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var message = await result.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return new TodoItem { Title = message };
            }
            else
            {
                return await result.Content.ReadFromJsonAsync<TodoItem>();
            }*/

        }

        public async Task<Ticket> UpdateToDoItem(Guid id, Ticket item)
        {
            HttpResponseMessage response = await _http.PutAsJsonAsync("TodoItems/{id}", item);
            return response.Content.ReadFromJsonAsync<Ticket>().Result;
            
        }
    }
}