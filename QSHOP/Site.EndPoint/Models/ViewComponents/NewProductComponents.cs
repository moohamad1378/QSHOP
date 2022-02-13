using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBase;

namespace Site.EndPoint.Models.ViewComponents
{
    public class NewProductComponents:ViewComponent
    {
        private readonly DataBaseContext _context;
        public NewProductComponents(DataBaseContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var data = _context.CatalogItems
                .Include(p => p.Images)
                .OrderByDescending(p => p.Id)
                .Select(p => new Dto
                {
                    Id = p.Id,
                    Des = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    Slug = p.Slug,
                    Src = p.Images.FirstOrDefault().Src,

                }).ToList().Take(8);
            return View(viewName: "NewProductComponents", model: data);


        }
    }
    public class Dto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Src { get; set; }
        public int Price { get; set; }
        public string Des { get; set; }
    }
}
