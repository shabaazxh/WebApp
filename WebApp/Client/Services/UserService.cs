using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using WebApp.Shared;
using System.Threading.Tasks;

namespace WebApp.Client.Services
{
    public class UserService : IUserService
    {

        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<User> LogUserIn(User account)
        {
            var result = await _http.PostAsJsonAsync("User", account);
            return await result.Content.ReadFromJsonAsync<User>();
        }

        public async Task<User> RegisterUser(User addUser)
        {
            var result = await _http.PostAsJsonAsync("User/register", addUser);
            return await result.Content.ReadFromJsonAsync<User>();
        }

/*        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await _http.GetFromJsonAsync<List<ApplicationUser>>("UsersController");
        }*/
    }
}