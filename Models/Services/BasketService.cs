    using Microsoft.AspNetCore.Identity;
    using sellhandproduct.Data;
    using sellhandproduct.Models.Domain;

    namespace sellhandproduct.Models.Services
    {
        public class BasketService
        {
            private readonly MVCDemoDbContext _mvcdemodbcontext;
            private readonly MVCDataContext _mvcDataContext;

            public BasketService(MVCDemoDbContext mvcdemodbcontext, MVCDataContext mVCDataContext)
            {
                _mvcdemodbcontext = mvcdemodbcontext;
                _mvcDataContext = mVCDataContext;
            }

        public void AddProductToBasket(string userId, int productId)
        {
            var product = _mvcDataContext.Products.FirstOrDefault(p => p.Id == productId);
            var basket = _mvcdemodbcontext.Basket.FirstOrDefault(b => b.UserId == userId);

            if (product != null && basket != null)
            {
                basket.Product.Add(product);
                _mvcDataContext.SaveChanges();
                _mvcdemodbcontext.SaveChanges();
            }
        }

    }
}
