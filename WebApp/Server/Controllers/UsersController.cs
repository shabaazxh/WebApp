
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Server.Data;
using WebApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Server.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Models.ApplicationUser> userManager;

        public UsersController(ApplicationDbContext context, UserManager<Models.ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Models.ApplicationUser>> Get()
        {
            return userManager.Users.ToList<Models.ApplicationUser>();
        }


    }
       
}