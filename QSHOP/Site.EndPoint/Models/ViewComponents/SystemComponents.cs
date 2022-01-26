using Application.Services.Catalogs.System.SystemServices;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Models.ViewComponents
{
    public class SystemComponents:ViewComponent
    {
        private readonly ISystemServices _systemServices;
        public SystemComponents(ISystemServices systemServices)
        {
            _systemServices = systemServices;
        }
        public IViewComponentResult Invoke()
        {
            var data = _systemServices.List();
            return View(viewName:"SystemComponents",model:data);
        }
    }
}
