using Application.Services.OurBrandService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class OurBrandController : Controller
    {
        private readonly IOurBrandService _ourBrandService;
        public OurBrandController(IOurBrandService ourBrandService)
        {
            _ourBrandService= ourBrandService;
        }
        public IActionResult Index()
        {
            var data=_ourBrandService.List();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddOurBrandDto addOurBrandDto)
        {
            _ourBrandService.Add(addOurBrandDto);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            _ourBrandService.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
