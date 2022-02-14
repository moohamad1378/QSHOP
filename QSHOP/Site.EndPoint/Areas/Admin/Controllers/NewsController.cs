using Application.Services.NewsServices;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence.DataBase;

namespace Site.EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly INewsService _newsService;
        private readonly UserManager<User> _userManager;
        public NewsController(DataBaseContext context
            , INewsService newsService, UserManager<User> userManager)
        {
            _context = context;
            _newsService = newsService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var data=_newsService.GetListForAdmin();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddNewsDto addNewsDto)
        {
            var user=_userManager.FindByNameAsync(User.Identity.Name).Result;
            addNewsDto.UserId = user.Id;
            _newsService.Add(addNewsDto);

            return RedirectToAction("Index");
        }
        public EditNewsDto EditNewsDto = new EditNewsDto { };
        public IActionResult Edit(int Id)
        {
            var data = _context.News.FirstOrDefault(p => p.Id==Id);
            EditNewsDto.Title = data.Title;
            EditNewsDto.Description = data.Description;
            EditNewsDto.Id = Id;
            return View(EditNewsDto);
        }
        [HttpPost]
        public IActionResult Edit(EditNewsDto edit)
        {
            _newsService.Edit(edit);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            _newsService.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
