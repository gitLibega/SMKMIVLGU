using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMKMIVLGU.Models;
using SMKMIVLGU.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMKMIVLGU.Controllers.statistics
{
	public class StatisticController : Controller
	{
		UserManager<User> _userManager;
		ApplicationDbContext _db;
		public StatisticController(UserManager<User> userManager, ApplicationDbContext db)
		{
			_userManager = userManager;
			_db = db;
		}

		[HttpGet]
		public IActionResult ShowStatistic()
		{
			ViewBag.Years = _db.UserReports.Select(p => p.LoadTime.Year).Distinct().ToList();
			ViewBag.Months = _db.UserReports.Select(p => p.Season).Distinct().ToList() ;

			var allIks = new List<int>();
	
				 allIks = _db.UserReports.Select(p => p.IkProcessId).Distinct().ToList();			
		
			List<reportStatisticViewModel> result = new List<reportStatisticViewModel>();
			foreach (var id in allIks)
			{
				var ik = _db.IkProcesses.Where(w => w.id == id).FirstOrDefault();
				var average = _db.UserReports.Where(k => k.IkProcessId == id).Select(p => new StatisticViewModel
				{
					CodeIk = p.IkProcess.СodeIkProcess,
					RSMK = p.IkProcess.RSMK,
					AverageCoef = p.AverageCoef
				}).Average(n => n.AverageCoef);
			
				result.Add(new reportStatisticViewModel { CodeIk = ik.СodeIkProcess, RSMK = ik.RSMK, AverageCoef = (int)average });
			}
			StatisticViewModel model = new StatisticViewModel();
			model.listItems = result;
			return View(model);
		}
		[HttpPost]
		public IActionResult ShowStatistic(StatisticViewModel model)
		{
			string month = model.Period;
			string year = model.Date;
			ViewBag.Years = _db.UserReports.Select(p => p.LoadTime.Year).Distinct().ToList();
			ViewBag.Months = _db.UserReports.Select(p => p.Season).Distinct().ToList();
			List<reportStatisticViewModel> result = new List<reportStatisticViewModel>();
			var allIks = new List<int>();
			if (month == "Все" && year == "Все")
			{
				allIks = _db.UserReports.Select(p => p.IkProcessId).Distinct().ToList();
				foreach (var id in allIks)
				{
					var ik = _db.IkProcesses.Where(w => w.id == id).FirstOrDefault();
					var average = _db.UserReports.Where(k => k.IkProcessId == id).Select(p => new StatisticViewModel
					{
						CodeIk = p.IkProcess.СodeIkProcess,
						RSMK = p.IkProcess.RSMK,
						AverageCoef = p.AverageCoef
					}).Average(n => n.AverageCoef);
					result.Add(new reportStatisticViewModel { CodeIk = ik.СodeIkProcess, RSMK = ik.RSMK, AverageCoef = (int)average });
				}
			}
			else if (month != "Все" && year != "Все")
			{
				allIks = _db.UserReports.Where(p => p.LoadTime.Year.ToString() == year).Where(p => p.Season == month).Select(i => i.IkProcessId).Distinct().ToList();
				foreach (var id in allIks)
				{
					var ik = _db.IkProcesses.Where(w => w.id == id).FirstOrDefault();
					var average = _db.UserReports.Where(k => k.IkProcessId == id).Where(p => p.Season == month).Where(p => p.LoadTime.Year.ToString() == year).Select(p => new StatisticViewModel
					{
						CodeIk = p.IkProcess.СodeIkProcess,
						RSMK = p.IkProcess.RSMK,
						AverageCoef = p.AverageCoef
					}).Average(n => n.AverageCoef);
					result.Add(new reportStatisticViewModel { CodeIk = ik.СodeIkProcess, RSMK = ik.RSMK, AverageCoef = (int)average });
				}
			}
			else if (month == "Все" && year != "Все")
			{
				allIks = _db.UserReports.Where(p => p.LoadTime.Year.ToString() == year).Select(i => i.IkProcessId).Distinct().ToList();
				foreach (var id in allIks)
				{
					var ik = _db.IkProcesses.Where(w => w.id == id).FirstOrDefault();
					var average = _db.UserReports.Where(k => k.IkProcessId == id).Where(p => p.LoadTime.Year.ToString() == year).Select(p => new StatisticViewModel
					{
						CodeIk = p.IkProcess.СodeIkProcess,
						RSMK = p.IkProcess.RSMK,
						AverageCoef = p.AverageCoef
					}).Average(n => n.AverageCoef);
					result.Add(new reportStatisticViewModel { CodeIk = ik.СodeIkProcess, RSMK = ik.RSMK, AverageCoef = (int)average });
				}
			}
			else if (month != "Все" && year == "Все")
			{
				allIks = _db.UserReports.Where(p => p.Season == month).Select(i => i.IkProcessId).Distinct().ToList();
				foreach (var id in allIks)
				{
					var ik = _db.IkProcesses.Where(w => w.id == id).FirstOrDefault();
					var average = _db.UserReports.Where(k => k.IkProcessId == id).Where(p => p.Season == month).Select(p => new StatisticViewModel
					{
						CodeIk = p.IkProcess.СodeIkProcess,
						RSMK = p.IkProcess.RSMK,
						AverageCoef = p.AverageCoef
					}).Average(n => n.AverageCoef);
					result.Add(new reportStatisticViewModel { CodeIk = ik.СodeIkProcess, RSMK = ik.RSMK, AverageCoef = (int)average });
				}
			}
			
		
			model.listItems = result;
			return View(model);
		}

	}
}
