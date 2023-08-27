using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sellhandproduct.Data;
using sellhandproduct.Models;
using sellhandproduct.Models.ViewModel;
using System.Diagnostics;

namespace sellhandproduct.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MVCDemoDbContext _mvcdemodbcontext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MVCDataContext _mvcDataContext;

        public HomeController(ILogger<HomeController> logger,UserManager<IdentityUser> userManager, MVCDemoDbContext mvcdemodbcontext, MVCDataContext mVCDataContext)
        {
            _logger = logger;
            _userManager = userManager;
            _mvcdemodbcontext = mvcdemodbcontext;
            _mvcDataContext = mVCDataContext;

        }

        public async Task<IActionResult> IndexAsync()
        {
            var products = await _mvcDataContext.Products.ToListAsync();
            var users = await _mvcdemodbcontext.applicationUsers.ToListAsync();

            var viewModel = new MyViewModel
            {
                Products = products,
                Users = users
            };
            return View(viewModel);

            

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}