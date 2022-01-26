using Application.Services.Catalogs.CatalogBrands;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Models.ViewComponents
{
    public class BrandComponents: ViewComponent
    {
        private readonly ICatalogBrandService _catalogBrandService;
        public BrandComponents(ICatalogBrandService catalogBrandService)
        {
            _catalogBrandService=catalogBrandService;
        }
        public IViewComponentResult Invoke()
        {
            var Brand = _catalogBrandService.List();
            return View(viewName: "BrandComponents", model: Brand);
        }
    }
}
