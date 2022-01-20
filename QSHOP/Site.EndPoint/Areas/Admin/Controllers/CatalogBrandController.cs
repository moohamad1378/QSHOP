using Application.Services.Catalogs.CatalogBrands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class CatalogBrandController : Controller
    {
        private readonly ICatalogBrandService _catalogTypeService;
        public CatalogBrandController(ICatalogBrandService catalogTypeService)
        {
            _catalogTypeService = catalogTypeService;
        }
        public IActionResult Index()
        {

            var data = _catalogTypeService.List();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CatalogBranCreateDto catalogBranCreateDto)
        {
            string message = "";
            var result=_catalogTypeService.Add(catalogBranCreateDto);
            if (!result.IsSuccess)
            {
                TempData["Message"] = result.Message;
                return View();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int Id)
        {
            var data = _catalogTypeService.Find(Id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(FIndCatalogBrand fIndCatalogBrand)
        {
            _catalogTypeService.Edit(fIndCatalogBrand);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            _catalogTypeService.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
