using Application.Services.Baskets;
using Microsoft.AspNetCore.Mvc;
using Site.EndPoint.Models.Utility;
using System.Security.Claims;

namespace Site.EndPoint.Models.ViewComponents
{
    public class BasketComponents:ViewComponent
    {
        private readonly IBasketService _basketService;
        public BasketComponents(IBasketService basketService)
        {
            _basketService=basketService;
        }
        private ClaimsPrincipal userclaimPrincipal => ViewContext?.HttpContext?.User;
        public IViewComponentResult Invoke()
        {
            BasketDto basket = null;
            if (User.Identity.IsAuthenticated)
            {
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
