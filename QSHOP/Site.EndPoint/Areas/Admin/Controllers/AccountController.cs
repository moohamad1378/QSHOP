using Common.Dtos;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence.DataBase;

namespace Site.EndPoint.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly DataBaseContext _context;
        public AccountController(UserManager<User> userManager,DataBaseContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _userManager.Users.Select(p => new Common.Dtos.UserListDto
            {
                Email = p.Email,
                Id = p.Id,
                UserName = p.UserName
            }).ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegisterDto);
            }
            User user = new User
            {
                Email = userRegisterDto.Email,
                UserName = userRegisterDto.UserName,
            };
            var result= _userManager.CreateAsync(user, userRegisterDto.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string message = "";
                foreach (var item in result.Errors.ToList())
                {
                    message += item.Description + Environment.NewLine;
                }
                TempData["Message"] = message;
            }

            return View();
        }
        public IActionResult Delete(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            var resutl = _userManager.DeleteAsync(user).Result;
            return RedirectToAction("Index");
        }
        public IActionResult Edit(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            EditUserDto user1 = new EditUserDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Id=user.Id,
            };
            return View(user1);
        }
        [HttpPost]
        public IActionResult Edit(EditUserDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegisterDto);
            }
            var find = _userManager.FindByIdAsync(userRegisterDto.Id).Result;


            find.UserName = userRegisterDto.UserName;
            find.Email = userRegisterDto.Email;
            
            var edit =_userManager.UpdateAsync(find).Result;
            return RedirectToAction("Index");
        }
    }
}
