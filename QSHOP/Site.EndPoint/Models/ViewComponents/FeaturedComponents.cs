using Application.Services.Catalogs.CatalogItems;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Models.ViewComponents
{
    public class FeaturedComponents:ViewComponent
    {
        private readonly ICatalogItemService _catalogItemService;
        public FeaturedComponents(ICatalogItemService catalogItemService)
        {
            _catalogItemService = catalogItemService;
        }
        public IViewComponentResult Invoke()
        {
            var mode = _catalogItemService.getVizhe();
            return View(viewName: "FeaturedComponents", model: mode);
        }
    }
}
