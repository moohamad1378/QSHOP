using Application.Services.Baskets;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.EndPoint.Models.Utility;
using System.Security.Claims;

namespace Site.EndPoint.Models.ViewComponents
{
    public class BasketComponents:ViewComponent
    {
        private readonly IBasketService _basketService;
        private readonly UserManager<User> _userManager;
        public BasketComponents(IBasketService basketService
            , UserManager<User> userManager)
        {
            _basketService=basketService;
            _userManager = userManager;
        }
        private ClaimsPrincipal userclaimPrincipal => ViewContext?.HttpContext?.User;
        public IViewComponentResult Invoke()
        {
            BasketDto basket = null;
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                basket = _basketService.GetBasketForUser(ClaimUtility.GetUserId(userclaimPrincipal));
            }
            else
            {
                string BasketCookieName = "BasketId";
                if (Request.Cookies.ContainsKey(BasketCookieName))
                {
                    var buyerID = Request.Cookies[BasketCookieName];
                    basket = _basketService.GetBasketForUser(buyerID);
                }
            }
            return View(viewName: "BasketComponents", model: basket);
        }
    }
}
