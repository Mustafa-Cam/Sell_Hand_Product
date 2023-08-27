using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sellhandproduct.Data;
using sellhandproduct.Models.Domain;
//using sellhandproduct.Models.Services;
using System.Collections.Generic; // ICollection için gerekli using direktifi

namespace sellhandproduct.Controllers
{
    public class BasketController : Controller
    {
        private readonly MVCDemoDbContext _mvcdemodbcontext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MVCDataContext _mvcDataContext;
        //private readonly BasketService _basketService;
        //private object BasketService _basketService;

        public BasketController(UserManager<IdentityUser> userManager, MVCDemoDbContext mvcdemodbcontext, MVCDataContext mVCDataContext)
        {
            _userManager = userManager;
            _mvcdemodbcontext = mvcdemodbcontext;
            _mvcDataContext = mVCDataContext;
            //_basketService = basketService;
        }




        [HttpPost]
        public IActionResult AddToBasket(int productId)
        {
            var product = _mvcDataContext.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User); // Kullanıcı kimliğini almak için UserManager kullanın
            var basket = _mvcdemodbcontext.Basket.FirstOrDefault(b => b.UserId == userId);

            if (basket == null)
            {
                basket = new Baskets
                {
                    UserId = userId,
                    Product = new List<Products>()
                };
                _mvcdemodbcontext.Basket.Add(basket);
            }

            basket.Product.Add(product); 
            _mvcDataContext.SaveChanges();
            _mvcdemodbcontext.SaveChanges();

            return RedirectToAction("Index", "Basket"); // Sepet sayfasına yönlendir
        }


        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var basket = _mvcdemodbcontext.Basket
                .Include(b => b.Product)
                .FirstOrDefault(b => b.UserId == userId);

            return View(basket);
        }


    }
}