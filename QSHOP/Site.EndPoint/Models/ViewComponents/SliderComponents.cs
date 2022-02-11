using Application.Services.HomeServices.Sliders;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Models.ViewComponents
{
    public class SliderComponents:ViewComponent
    {
        private readonly ISliderService _sliderService;
        public SliderComponents(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public IViewComponentResult Invoke()
        {
            var Model = _sliderService.GetList();
            return View(viewName: "SliderComponents", model: Model);
        }
    }
}
