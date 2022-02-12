using Application.Services.Catalogs.CatalogBrands;
using Application.Services.Catalogs.CatalogItems;
using Application.Services.Catalogs.CatalogTypss;
using Application.Services.Catalogs.System.SystemServices;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Persistence.DataBase;

namespace Site.EndPoint.Areas.Admin.Controllers
{
    [Authorize(Roles =" Admin , Operator")]
    [Area("Admin")]
    public class CatalogItemController : Controller
    {
        private readonly ICatalogTypssServices _catalogTypssServices;
        private readonly ICatalogBrandService _catalogBrandService;
        private readonly ICatalogItemService _catalogItemService;
        private readonly UserManager<User> _userManager;
        private readonly DataBaseContext _context;
        private readonly ISystemServices _systemServices;
        public CatalogItemController(ICatalogTypssServices catalogTypssServices
            , ICatalogBrandService catalogBrandService, ICatalogItemService catalogItemService
            , UserManager<User> userManager,DataBaseContext context
            , ISystemServices systemServices)
        {
            _catalogTypssServices = catalogTypssServices;
            _catalogBrandService = catalogBrandService;
            _catalogItemService = catalogItemService;
            _userManager = userManager;
            _context = context;
            _systemServices = systemServices;
        }
        public IActionResult Index(CatlogPLPRequestDto catlogPLPRequestDto)
        {
            var data = _catalogItemService.ListForAdmin(catlogPLPRequestDto);
            return View(data);
        }
        public IActionResult Create(int? Id)
        {
            ViewBag.catalogtype = new SelectList(_catalogTypssServices?.GetListForAdmin(), "Id", "Type");
            ViewBag.catalogbrand = new SelectList(_catalogBrandService?.List(), "Id", "Brand");
            ViewBag.Systems = new SelectList(_systemServices?.List(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(CatalogItemCreadeDto catalogItemCreadeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(catalogItemCreadeDto);
            }
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            catalogItemCreadeDto.UserId = user.Id;
            var data=_catalogItemService.AddCatalogItem(catalogItemCreadeDto);
            return RedirectToAction("CreateColor", new {Id=data});
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            _catalogItemService.Delete(Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var data = _context.CatalogItems.FirstOrDefault(p => p.Id == Id);
            EditCatalogItemDto editCatalogItemDto = new EditCatalogItemDto()
            {
                Id = Id,
                AvailableStock = data.AvailableStock,
                DeliveryDetail = data.DeliveryDetail,
                Description = data.Description,
                Detail = data.Detail,
                Name = data.Name,
                Price = data.Price,
                MaxStockThreshold = data.MaxStockThreshold,
                RestockThreshold = data.RestockThreshold
            };
            ViewBag.catalogtype = new SelectList(_catalogTypssServices.GetListForAdmin(), "Id", "Type");
            ViewBag.catalogbrand = new SelectList(_catalogBrandService.List(), "Id", "Brand");
            return View(editCatalogItemDto);
        }
        [HttpPost]
        public IActionResult Edit(EditCatalogItemDto editCatalogItemDto)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var deleteimage = _context.Images.Where(p => p.CataLogItemId == editCatalogItemDto.Id);
            _context.Images.RemoveRange(deleteimage);
            _context.SaveChanges();
            editCatalogItemDto.UserId = user.Id;
            _catalogItemService.Edit(editCatalogItemDto);
            return RedirectToAction("Index");
        }
        public CreateColorDto CreateColorDto { get; set; } = new CreateColorDto { };
        public IActionResult CreateColor(int Id)
        {
            ViewBag.ColorList = new SelectList(_catalogItemService.ListOfColor(Id),"Id","Name");
            CreateColorDto.CatalogItemId = Id;
            return View(CreateColorDto);
        }
        [HttpPost]
        public IActionResult CreateColor(CreateColorDto createColorDto)
        {
            _catalogItemService.AddColor(createColorDto);
            return RedirectToAction("CreateColor");
        }
        public IActionResult DeleteColor(int Id)
        {
            _catalogItemService.DeleteColor(Id);
            return RedirectToAction("Index");
        }
        public EditColorDto EditColorDto { get; set; } = new EditColorDto { };
        [HttpGet]
        public IActionResult EditColor(int Id)
        {
            EditColorDto.Id = Id;
            return View(EditColorDto);
        }

        [HttpPost]
        public IActionResult EditColor(EditColorDto editColorDto)
        {
            _catalogItemService.EditColor(editColorDto);
            return RedirectToAction("Index");
        }
        public CreateMaterialDto CreateMaterialDto { get; set; } = new CreateMaterialDto { };
        [HttpGet]
        public IActionResult CreateMaterial(int Id)
        {
            CreateMaterialDto.CatalogItemId = Id;
            ViewBag.ListOfMaterial = new SelectList(_catalogItemService.ListOfMaterial(Id), "Id", "Name");
            return View(CreateMaterialDto);
        }
        [HttpPost]
        public IActionResult CreateMaterial(CreateMaterialDto createMaterialDto)
        {
            var data = _catalogItemService.AddMaterial(createMaterialDto);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteMaterial(int Id)
        {
            _catalogItemService.DeleteMateriald(Id);
            return RedirectToAction("Index");
        }
        public EditMaterialDto editmaterialdto { get; set; } = new EditMaterialDto { };
        [HttpGet]
        public IActionResult EditMaterial(int Id)
        {
            editmaterialdto.Id = Id;
            return View(editmaterialdto);
        }
        [HttpPost]
        public IActionResult EditMaterial(EditMaterialDto editMaterialDto)
        {
            _catalogItemService.EditMateriald(editMaterialDto);
            return RedirectToAction("Index");
        }
        public IActionResult Systems()
        {
            var data = _systemServices.List();
            return View(data);
        }
        [HttpGet]
        public IActionResult AddSystem()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult AddSystem(CreateSystemDto createSystemDto)
        {
            var data = _systemServices.Add(createSystemDto);
            return RedirectToAction("Systems");
        }
        [HttpGet]
        public IActionResult DeleteSystem(int Id)
        {
            var data = _systemServices.Delete(Id);
            return RedirectToAction("Systems");
        }
        public EditSystemDto EditSystemDto { get; set; } = new EditSystemDto { };
        [HttpGet]
        public IActionResult EditSystem(int Id)
        {
            EditSystemDto.Id = Id;
            return View(EditSystemDto);
        }
        [HttpPost]
        public IActionResult EditSystem(EditSystemDto editSystemDto)
        {
            var data = _systemServices.Edit(editSystemDto);
            return RedirectToAction("Systems");
        }
        public IActionResult IsViZhe(int Id)
        {
            _catalogItemService.IsViZhe(Id);

            return RedirectToAction("Index");
        }
    }
}
