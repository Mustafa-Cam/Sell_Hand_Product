using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sellhandproduct.Data;
using sellhandproduct.Models;
using sellhandproduct.Models.Domain;
using sellhandproduct.Models.ViewModel;

namespace sellhandproduct.Controllers
{
    
    public class ProductsController : Controller
    {
        private readonly MVCDemoDbContext _mvcdemodbcontext;
        private readonly UserManager<IdentityUser> _userManager; 
        private readonly MVCDataContext _mvcDataContext;

        public ProductsController(UserManager<IdentityUser> userManager, MVCDemoDbContext mvcdemodbcontext,MVCDataContext mVCDataContext)
        {
            _userManager = userManager;
            _mvcdemodbcontext = mvcdemodbcontext;
            _mvcDataContext = mVCDataContext;
        }

        [HttpGet]
        [Authorize(Roles = "Seller,Admin,User")]

        public async Task<IActionResult> ProductList()  // bak ıactionresult dan sonraki kısım yani fonksiyon kısmımızın kesinlikle view de bir karşılığı olmalı
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
        [HttpGet]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> MyProducts()
        {
            var users = await _mvcdemodbcontext.applicationUsers.ToListAsync();

            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Account"); // Kullanıcı giriş yapmamışsa giriş sayfasına yönlendir
            }

            var sellerId = loggedInUser.Id; // Bu satırda ApplicationUser sınıfınıza uygun şekilde satıcı ID'si özelliğini kullanmalısınız

            var products = await _mvcDataContext.Products
                .Where(p => p.SellerId == sellerId)
                .ToListAsync();

            var viewModel = new MyViewModel
            {
                Products = products,
                Users = users 
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ProductDetail(UpdateProductViewModel model) // post geliyor aa burda UpdateEmployeeViewModel varmış verileri buna atayım diyor kısaca
        {
            var product = await _mvcDataContext.Products.FindAsync(model.ProductId); 

            if (product != null)

            {
                product.Id = model.ProductId;
                product.ProductName= model.ProductName;
                product.ProductCount= model.ProductCount;
                product.ProductPrice= model.ProductPrice;
                
                await _mvcDataContext.SaveChangesAsync();
                return Redirect("ProductList");

            }
            return RedirectToAction("ProductList");
        }


        [HttpGet]
        [Authorize(Roles = "Seller")]
        public IActionResult AddProducts()
        {
            return View();

        }
        [HttpGet]
        public IActionResult Naber()
        {
            return View();

        }

        //basket get işlemi yapamadık
      



        [HttpGet]
        public async Task<IActionResult> ProductDetail(int id)
        {
            var users = await _mvcdemodbcontext.applicationUsers.ToListAsync();

            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Account"); // Kullanıcı giriş yapmamışsa giriş sayfasına yönlendir
            }
            var product = await _mvcDataContext.Products.FirstOrDefaultAsync(e => e.Id == id);
            var sellerId = loggedInUser.Id; // Bu satırda ApplicationUser sınıfınıza uygun şekilde satıcı ID'si özelliğini kullanmalısınız
            var products = await _mvcDataContext.Products
               .Where(p => p.SellerId == sellerId)
               .ToListAsync();


            if (product != null)
            {
                var viewModel = new UpdateProductViewModel()
                {
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductCount = product.ProductCount,
                    SellerId = product.SellerId,
                    Image = product.ImageData,

                    
                };
                return View(viewModel);
            }
            return RedirectToAction("ProductList"); // else kısmı 
        }

       
            // Diğer özellikler...

           


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _mvcDataContext.Products.FindAsync(id);
            if (product == null)
            {
                return Json(new { success = false }); // Ürün bulunamadı
            }

            _mvcDataContext.Products.Remove(product);
            await _mvcDataContext.SaveChangesAsync(); 

            return Json(new { success = true }); // Ürün başarıyla silindi
        }


        [HttpPost]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> AddProducts(AddProductViewModel addProductRequest)
        { 
            if (!ModelState.IsValid)   
            {
                
                if (User.Identity.IsAuthenticated) 
                {
                    var user = await _userManager.GetUserAsync(User); 
                    addProductRequest.SellerId = user.Id;

                    // Product eklerken SellerId'yi de veritabanına ekleyin
                    var product = new Products()
                    {
                        ProductName = addProductRequest.ProductName,
                        ProductPrice = addProductRequest.ProductPrice,
                        ProductCount = addProductRequest.ProductCount,
                        SellerId = addProductRequest.SellerId,
                        
                    };
                     if (addProductRequest.ImageFile != null)
                    {
                        product.SetImage(addProductRequest.ImageFile); // burada imagefile yüklediğimiz dosya resim olarak ayarlıyoruz
                    }
                    await _mvcDataContext.Products.AddAsync(product);
                    await _mvcDataContext.SaveChangesAsync(); 
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Lütfen önce giriş yapınız.");
                    return View(addProductRequest);
                }
            }
            else
            {
                    var errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                // ModelState geçerli değilse, View'e hemen geri dön
                return RedirectToAction("Naber");
            }

            // ModelState geçerliyse, ProductList sayfasına yönlendir
            return RedirectToAction("ProductList");
        }


    }
}
