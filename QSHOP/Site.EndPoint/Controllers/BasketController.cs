using Application.Services.Baskets;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly SignInManager<User> _signInManager;
        private string _userId = null;
        public BasketController(IBasketService basketService,
            SignInManager<User> signInManager)
        {
            _basketService = basketService;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var Basket = GetOrSetBasket();
            return View(Basket);
        }
        [HttpPost]
        public IActionResult Index(int CatalogItemId, int ColorId, int MaterialId, int Quantity = 1)
        {
            var Basket=GetOrSetBasket();
            _basketService.AddItemToBasket(Basket.Id, CatalogItemId, Quantity, ColorId, MaterialId);
            return RedirectToAction("Index");
        }
        private BasketDto GetOrSetBasket()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return _basketService.GetOrCreateBasketForUser(User.Identity.Name);
            }
            else
            {
                SetKiesForBasket();
                return _basketService.GetOrCreateBasketForUser(_userId);
            }
        }
        private void SetKiesForBasket()
        {
            string BasketCookieName = "BasketId";
            if (Request.Cookies.ContainsKey(BasketCookieName))
            {
                _userId=Request.Cookies[BasketCookieName];
            }
            if (_userId != null) return;
            _userId=Guid.NewGuid().ToString();
            var cookieOptions=new CookieOptions { IsEssential = true };
            cookieOptions.Expires=DateTime.Today.AddYears(5);
            Response.Cookies.Append(BasketCookieName,_userId,cookieOptions);
        }
    }
}
