﻿using Application.Services.Catalogs.CatalogItems;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICatalogItemService _catalogItemService;
        public ProductController(ICatalogItemService catalogItemService)
        {
            _catalogItemService = catalogItemService;
        }
        public IActionResult Index(CatlogPLPRequestDto catlogPLPRequestDto)
        {
            var data = _catalogItemService.ListForAdmin( catlogPLPRequestDto);
            return View(data);
        }

    }
}
