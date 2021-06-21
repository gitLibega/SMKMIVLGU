using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMKMIVLGU.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMKMIVLGU.Controllers
{
	public class RolesManangementController : Controller
	{
		RoleManager<IdentityRole> _roleManager;
		UserManager<User> _userManager;

		public RolesManangementController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
		}
		public IActionResult AddUserRole()
		{
			ViewBag.Roles = _roleManager.Roles.ToList();
			return View("AddUserRole");
		}
		[HttpPost]

		public async Task<IActionResult> AddUserRole(NewRoleViewModel model)
		{
			ViewBag.Roles = _roleManager.Roles.ToList();
			if (!string.IsNullOrEmpty(model.nameRole))
			{
				IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(model.nameRole));
				if (result.Succeeded)
				{
					return RedirectToAction("AddUserRole", "RolesManangement");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}

				}
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			IdentityRole role = await _roleManager.FindByIdAsync(id);
			if (role != null)
			{
				IdentityResult result = await _roleManager.DeleteAsync(role);
			}
			return RedirectToAction("AddUserRole", "RolesManangement");
		}



		public async Task<IActionResult> EditUserRole(string id)
		{

			// получаем пользователя
			User user = await _userManager.FindByIdAsync(id);
			var userRoles = await _userManager.GetRolesAsync(user);
			var allRoles = _roleManager.Roles.ToList();
			if (user == null)
			{
				return NotFound();
			}
			PermissionViewModel model = new PermissionViewModel { UserId = user.Id, UserName = user.UserName, UserRoles = userRoles, AllRoles = allRoles };
			return PartialView(model);
		}
		[HttpPost]
		public async Task<IActionResult> EditUserRole(string userId, List<string> roles)
		{
			// получаем пользователя
			User user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				// получем список ролей пользователя
				var userRoles = await _userManager.GetRolesAsync(user);
				// получаем все роли
				var allRoles = _roleManager.Roles.ToList();
				// получаем список ролей, которые были добавлены
				var addedRoles = roles.Except(userRoles);
				// получаем роли, которые были удалены
				var removedRoles = userRoles.Except(roles);

				await _userManager.AddToRolesAsync(user, addedRoles);

				await _userManager.RemoveFromRolesAsync(user, removedRoles);

				return RedirectToAction("UserPannel", "Home");
			}

			return NotFound();
		}
	}
}

