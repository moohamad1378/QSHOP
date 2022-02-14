using Application.Services.OurBrandService;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Models.ViewComponents
{
    public class OurBrandComponents:ViewComponent
    {
        private readonly IOurBrandService _ourBrandService;
        public OurBrandComponents(IOurBrandService ourBrandService)
        {
            _ourBrandService = ourBrandService;
        }
        public IViewComponentResult Invoke()
        {
            var data = _ourBrandService.List();
            return View(viewName: "OurBrandComponents", model: data);
        }
    }
}
