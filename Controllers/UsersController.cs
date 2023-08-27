using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sellhandproduct.Data;
using sellhandproduct.Models; 

namespace sellhandproduct.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly MVCDemoDbContext ccontext;
        public UsersController(MVCDemoDbContext context )
        {
            ccontext = context; 
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var users = await ccontext.applicationUsers.ToListAsync();
            return View(users);
        }
    }
}
