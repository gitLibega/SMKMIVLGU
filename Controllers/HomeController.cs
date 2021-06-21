using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMKMIVLGU.Models;
using SMKMIVLGU.Models.IKProcess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SMKMIVLGU.Controllers
{

	public class HomeController : Controller
	{

		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly ApplicationDbContext _db;
		public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext db)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_db = db;

		}

		[HttpGet]
		public IActionResult History()
		{
			string userId = _userManager.GetUserAsync(User).Result.Id;
			var rels = (from rel in _db.UserRelationships
						join ward in _db.Users on rel.idWard equals ward.Id
						where rel.idUser == userId
						select ward.Department).ToList();

			var reports = (from report in _db.UserReports
						   join user in _db.Users on report.UserId equals user.Id
						   join ik in _db.IkProcesses on report.IkProcessId equals ik.id
						   select new HistoryViewModel
						   {
							   id = report.Id,
							   Department = user.Department,
							   ikProcess = $"{ik.СodeIkProcess}-{ik.RSMK}",
							   loadTime = report.LoadTime
						   }).ToList();

			ViewBag.myReports = reports.Where(p => p.Department == _userManager.GetUserAsync(User).Result.Department).ToList();
			var relReports = new List<HistoryViewModel>();
			foreach (var reportRel in reports)
			{
				if (rels.Contains(reportRel.Department))
				{
					relReports.Add(reportRel);

				}

			}
			ViewBag.relsReports = relReports;
			return View(reports);

		}
		public IActionResult UserPannel()
		{
			ViewBag.Users = _userManager.Users.ToList();
			var Item = _userManager.Users.ToList();
			var ikProcess = _db.IkProcesses.ToList();
			RegistrationViewModel rm = new RegistrationViewModel();
			rm.Relationships = Item.Select(vm => new CheckBoxItem()
			{
				id = vm.Id,
				Title = vm.FIO + $"({vm.Department})",
				isChecked = false


			}).ToList();
			rm.IkProcesses = ikProcess.Select(vm => new CheckBoxItem()
			{
				id = vm.id.ToString(),
				Title = vm.СodeIkProcess + $"({vm.RSMK})",
				isChecked = false


			}).ToList();
			return View(rm);
		}

		[HttpGet]
		public IActionResult Report(int id = 0)
		{
			string userName = User.Identity.Name;
			var iksUser = _db.UserIkProcessRels.Where(p => p.UserId == (_userManager.Users.Where(k => k.UserName == userName).FirstOrDefault().Id));
			var iksInfo = new List<IkProcessViewModel>();
			if (iksUser.ToList().Count > 0)
			{
				foreach (var ikId in iksUser)
				{
					iksInfo.Add(_db.IkProcesses.Where(p => p.id == ikId.IkProcessid).FirstOrDefault());
				}
			}
			ViewBag.iks = iksInfo;
			var idIkRows = _db.configurationPatterns.Where(p => p.IkProcessId == id).ToList();
			var IkBody = _db.configurationPatterns.Select(cp => new PatternIkProcessRowViewModel
			{
				RowHtmlCode = cp.PatternIkProcessRow.RowHtmlCode
			}).FirstOrDefault();


			if (id != 0)
			{
				ViewBag.ikName = _db.IkProcesses.Where(p => p.id == id).FirstOrDefault();
				return View(IkBody);
			}
			else
			{
				return View();
			}
		}
		[HttpPost]
		public async Task<IActionResult> Report(string keks, int ikId, string season, int average)
		{

			if (ModelState.IsValid)
			{
				var time = DateTime.Now;

				var id = _userManager.GetUserAsync(User).Result.Id;
				ReportViewModel model = new ReportViewModel { UserId = id, IkProcessId = ikId, RowHtmlCode = keks, Season = season, LoadTime = time, AverageCoef = average };
				await _db.UserReports.AddAsync(model);
				await _db.SaveChangesAsync();
				return RedirectToAction("Report");
			}
			return View();
		}
		//[HttpGet]
		//public IActionResult EditReportThisUser(int id = 0)
		//{

		//	string userName = User.Identity.Name;
		//	var iksUser = _db.UserIkProcessRels.Where(p => p.UserId == (_userManager.Users.Where(k => k.UserName == userName).FirstOrDefault().Id));
		//	var iksInfo = new List<IkProcessViewModel>();
		//	if (iksUser.ToList().Count > 0)
		//	{
		//		foreach (var ikId in iksUser)
		//		{
		//			iksInfo.Add(_db.IkProcesses.Where(p => p.id == ikId.IkProcessid).FirstOrDefault());
		//		}
		//	}
		//	ViewBag.iks = iksInfo;
		//	var idIkRows = _db.configurationPatterns.Where(p => p.IkProcessId == id).ToList();
		//	var IkBody = _db.configurationPatterns.Select(cp => new PatternIkProcessRowViewModel
		//	{
		//		RowHtmlCode = cp.PatternIkProcessRow.RowHtmlCode
		//	}).FirstOrDefault();


		//	if (id != 0)
		//	{
		//		ViewBag.ikName = _db.IkProcesses.Where(p => p.id == id).FirstOrDefault();
		//		return View(IkBody);
		//	}
		//	else
		//	{
		//		return View();
		//	}
		//}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
