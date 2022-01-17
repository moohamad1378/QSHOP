using Common.Dtos;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Site.EndPoint.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly IAuthorizationService _authorizationService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager
            , IAuthorizationService authorizationService)
        {
            _userManager=userManager;
            _SignInManager = signInManager;
            _authorizationService=authorizationService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserRegisterDto userDto)
        {

            if (!ModelState.IsValid)
            {
                return View(userDto);
            }

            User user = new User()
            {
                UserName = userDto.UserName,
                PasswordHash = userDto.Password,
                Email = userDto.Email,
            };

            var usercreated = _userManager.CreateAsync(user, userDto.Password).Result;

            if (usercreated.Succeeded)
            {
                var findeduser=_userManager.FindByNameAsync(user.UserName).Result;
                var logined = _SignInManager.PasswordSignInAsync(findeduser, findeduser.PasswordHash, true, true).Result;
                _SignInManager.SignInAsync(findeduser, true);
                return RedirectToAction("Index", "Home");
            }

            string message = "";
            foreach (var item in usercreated.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;

            return View(userDto);
            return View();
        }
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginDto);
            }
            var users = _userManager.FindByNameAsync(userLoginDto.UserName).Result;
            if (users == null)
            {
                ViewBag.Error = "نام کاربری یا کلمه عبور اشتباه است";
                return View(userLoginDto);
            }
            var result = _SignInManager.PasswordSignInAsync(users, userLoginDto.Password, true, true).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult logout()
        {
            var logout = _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

}
