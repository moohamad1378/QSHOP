using Application.Services.HomeServices.Sliders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        public HomeController(ISliderService sliderService)
        {
            _sliderService=sliderService;
        }
        public IActionResult Index()
        {
            var data = _sliderService?.GetList();
            return View(data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddSliderDto addSliderDto)
        {
            _sliderService.Add(addSliderDto);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            _sliderService.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
