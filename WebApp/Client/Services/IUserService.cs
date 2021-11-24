
using WebApp.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Server.Models;

namespace WebApp.Client.Services
{
    interface IUserService
    {
        Task<User> LogUserIn(User account);
        Task<User> RegisterUser(User addUser);

        //Task<List<ApplicationUser>> GetUsers();
    }
}
