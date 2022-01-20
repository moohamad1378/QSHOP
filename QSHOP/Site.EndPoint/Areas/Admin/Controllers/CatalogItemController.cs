using Application.Services.Catalogs.CatalogBrands;
using Application.Services.Catalogs.CatalogItems;
using Application.Services.Catalogs.CatalogTypss;
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
        public CatalogItemController(ICatalogTypssServices catalogTypssServices
            , ICatalogBrandService catalogBrandService, ICatalogItemService catalogItemService
            , UserManager<User> userManager,DataBaseContext context)
        {
            _catalogTypssServices = catalogTypssServices;
            _catalogBrandService = catalogBrandService;
            _catalogItemService = catalogItemService;
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _catalogItemService.ListForAdmin();
            return View(data);
        }
        public IActionResult Create(int? Id)
        {
            ViewBag.catalogtype = new SelectList(_catalogTypssServices.GetListForAdmin(), "Id", "Type");
            ViewBag.catalogbrand = new SelectList(_catalogBrandService.List(), "Id", "Brand");
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
    }
}
