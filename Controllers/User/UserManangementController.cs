using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SMKMIVLGU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMKMIVLGU.Controllers
{
	public class UserManangementController : Controller
	{
        UserManager<User> _userManager;
        ApplicationDbContext _db;
        public UserManangementController(UserManager<User> userManager,ApplicationDbContext db )
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult EditUser()
        {
            ViewBag.Users = _userManager.Users.ToList();
            return PartialView();
        }


       


        public async Task<IActionResult> EditUserModal(string id)
        {
           
            User user = await _userManager.FindByIdAsync(id);
            var allUsers = _userManager.Users.ToList();
            var rels = _db.UserRelationships.ToList();
            var userRelationships = new List<string>();

            var alliks = _db.IkProcesses.ToList();
            var iks = _db.UserIkProcessRels.ToList();
            var userIks = new List<int>();

            foreach (var rel in rels)
			{
                if(rel.idUser==id)
				{
                    userRelationships.Add(rel.idWard);
				}
			}
            foreach (var ik in iks)
            {
                if (ik.UserId == id)
                {
                    userIks.Add(ik.IkProcessid);
                }
            }

            EditUserViewModel model = new EditUserViewModel { idUser=user.Id,
                FIO = user.FIO,
                Password = user.Password,
                Department=user.Department,
                Login=user.Login,
                AllUsers=allUsers,
                UserRelationships=userRelationships,
            AllIks=alliks,
            UserIks=userIks};
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserModal (EditUserViewModel model,List <string> rels, List<int> iks)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.idUser.ToString());
                if (user != null)
                {
                    var tempRels = _db.UserRelationships.ToList();
                    var tempIks = _db.UserIkProcessRels.ToList();


                    var userRelationships = new List<string>();
                    var userIkProcesses = new List<int>();

                    foreach (var rel in tempRels)
                    {
                        if (rel.idUser == model.idUser)
                        {
                            userRelationships.Add(rel.idWard);
                           
                        }
                    }
                    foreach (var rel in tempIks)
                    {
                        if (rel.UserId == model.idUser)
                        {
                           userIkProcesses.Add(rel.IkProcessid);
                        }
                    }

                    // получем список ролей пользователя

                    // получаем все роли


                    var _passwordValidator =
               HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;
                    user.FIO = model.FIO;
                    user.Login = model.Login;
                    user.Password = model.Password;
                    user.Department = model.Department;
                    
                    // получаем список связей, которые были добавлены
                    var addedRels = rels.Except(userRelationships).ToList();
                    // получаем связи, которые были удалены
                    var removedRelationships = userRelationships.Except(rels).ToList();

                    // получаем список ик-процессов, которые были добавлены
                    var addedIks = iks.Except(userIkProcesses).ToList();
                    // получаем ик-процессы, которые были удалены
                    var removedIkProcesses = userIkProcesses.Except(iks).ToList();

                    foreach (string idWard in addedRels)
                    {
                        _db.UserRelationships.Add(new UserRelationship { idUser = user.Id, idWard = idWard });
                    }
                    foreach (int idIk in addedIks)
                    {
                        _db.UserIkProcessRels.Add(new Models.IKProcess.UserIkProcessRelViewModel { UserId = user.Id, IkProcessid=idIk });
                    }
                    var removedRecordWithRels = new List<UserRelationship>();
                    var removedRecordWithIks = new List<Models.IKProcess.UserIkProcessRelViewModel>();
                    removedRecordWithRels = _db.UserRelationships.Where(p => p.idUser == user.Id).ToList();
                    removedRecordWithIks = _db.UserIkProcessRels.Where(p => p.UserId == user.Id).ToList();
                    var temp1 = new List<UserRelationship>();
                    var temp2 = new List<Models.IKProcess.UserIkProcessRelViewModel>();
                    foreach (var remRel in removedRelationships)
                    {
                       temp1.Add(removedRecordWithRels.Where(p=>p.idWard==remRel).FirstOrDefault());
                    }
                    foreach (var remRecord in temp1)
                    {
                        _db.UserRelationships.Remove(remRecord);

                    }
                    foreach (var remRel in removedIkProcesses)
                    {
                        temp2.Add(removedRecordWithIks.Where(p => p.IkProcessid == remRel).FirstOrDefault());
                    }
                    foreach (var remRecord in temp2)
                    {
                        _db.UserIkProcessRels.Remove(remRecord);

                    }
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        await _db.SaveChangesAsync();
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("UserPannel","Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                 
                }
            }
          
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("UserPannel", "Home");
        }
    }
}
	