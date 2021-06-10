using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using SMKMIVLGU.Models;
using SMKMIVLGU.Models.IKProcess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SMKMIVLGU.Controllers
{
	public class AuthController : Controller
	{
         private ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
       
       
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;

        }

        [HttpGet]
        public IActionResult AddUser()
        {
            ViewBag.Users = _userManager.Users.ToList();
           
            return PartialView();
            
         }
        [HttpPost]
        public async Task<IActionResult> AddUser(RegistrationViewModel model)
        {
            ViewBag.Users = _userManager.Users.ToList();
           
            if (ModelState.IsValid)
            {
                User user = new User { UserName= model.Login,Login = model.Login, FIO = model.FIO,Password=model.Password, Department=model.Department};
                // добавляем пользователя
                if (model.Relationships != null)
                {
                    foreach (var checkBox in model.Relationships)
                    {
                        if (checkBox.isChecked == true)
                        {
                            UserRelationship ur = new UserRelationship { idUser = user.Id, idWard = checkBox.id };
                            _db.UserRelationships.Add(ur);
                        }
                    }
                }
                if (model.IkProcesses != null)
                {
                    
                    foreach (var checkBox in model.IkProcesses)
                    {
                        if (checkBox.isChecked == true)
                        {
                                UserIkProcessRelViewModel ui = new UserIkProcessRelViewModel { IkProcessid = int.Parse(checkBox.id), UserId = user.Id };
                            _db.UserIkProcessRels.Add(ui);
                        }
                    }
                }


                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _db.SaveChanges();
                    // TempData["Toast"] = $"Пользователь {model.FIO} успешно добавлен!";
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("UserPannel", "Home");

                }
                else
                {
                   
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return RedirectToAction("UserPannel", "Home");
                }
            }
            return View("~/Views/Home/UserPannel.cshtml");
        }
        [HttpGet]
		public IActionResult Authorization(string returnUrl = null)
		{
			return View(new AuthorizationViewModel { ReturnUrl = returnUrl });
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authorization(AuthorizationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError($"", $"Неправильный логин и (или) пароль {model.Password}");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("authorization","Auth");
        }
    }
}
