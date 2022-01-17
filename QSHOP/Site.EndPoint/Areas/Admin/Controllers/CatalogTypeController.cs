using Application.Services.Catalogs.CatalogTypss;
using AutoMapper;
using Domain.Catalogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class CatalogTypeController : Controller
    {
        private readonly ICatalogTypssServices _catalogTypssServices;
        private readonly IMapper _mapper;
        public CatalogTypeController(ICatalogTypssServices catalogTypssServices
            , IMapper mapper)
        {
            _catalogTypssServices = catalogTypssServices;
            _mapper = mapper;
        }
        public IActionResult Index(int? parentid)
        {
            var result = _catalogTypssServices.GetList(parentid);
            return View(result);
        }

        public CatalogTypeDto CatalogTypedto { get; set; } = new CatalogTypeDto { };
        [HttpGet]
        public IActionResult Create(int? parentId)
        {
            CatalogTypedto.ParentCatalogTypeId = parentId;

            return View(CatalogTypedto);
        }

        [HttpPost]
        
        public IActionResult Create(CatalogTypeDto catalogTypeDto)
        {
            var model = _mapper.Map<CatalogTypeDto>(catalogTypeDto);
            var result = _catalogTypssServices.Add(model);

            return RedirectToAction("Index");
        }
        public IActionResult delete(int Id)
        {
            var resutl = _catalogTypssServices.Remove(Id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult edit(int Id)
        {
            var finded = _catalogTypssServices.FindById(Id);
            
            return View(finded.Data);
        }
        public IActionResult edit(CatalogTypeDto catalogTypeDto)
        {
            var result = _catalogTypssServices.Edit(catalogTypeDto);
            return RedirectToAction("Index");
        }
        public IActionResult Limit()
        {
            return View();
        }
    }
}
