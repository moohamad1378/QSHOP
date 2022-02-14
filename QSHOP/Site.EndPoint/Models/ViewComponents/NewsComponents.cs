using Application.Services.NewsServices;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Models.ViewComponents
{
    public class NewsComponents:ViewComponent
    {
        private readonly INewsService _newsService;
        public NewsComponents(INewsService newsService)
        {
            _newsService= newsService;
        }
        public IViewComponentResult Invoke()
        {
            var data = _newsService.GetListForSite();
            return View(viewName: "NewsComponents",model:data);
        }
    }
}
