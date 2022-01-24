using Application.Services.Catalogs.CatalogTypss;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Models.ViewComponents
{
    public class CategoryComponent: ViewComponent
    {
        private readonly ICatalogTypssServices _catalogTypssServices;
        public CategoryComponent(ICatalogTypssServices catalogTypssServices)
        {
            _catalogTypssServices = catalogTypssServices;
        }
        public IViewComponentResult Invoke()
        {
            var Typs = _catalogTypssServices.GetListForAdmin();
            return View(viewName: "CategoryComponent", model: Typs);
        }
    }
}
