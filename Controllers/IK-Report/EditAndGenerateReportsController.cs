using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMKMIVLGU.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SMKMIVLGU.Controllers.IK_Report
{
	public class EditAndGenerateReportsController : Controller
	{

		ApplicationDbContext _db;
		private readonly UserManager<User> _userManager;
		public EditAndGenerateReportsController(ApplicationDbContext db, UserManager<User> userManager)
		{
			_userManager = userManager;
			_db = db;
		}
		[HttpGet]
		public IActionResult GenerateReport(int id)
		{

			var report = _db.UserReports.Where(p => p.Id == id).FirstOrDefault();
			ViewData.Add("Process",_db.UserReports.Where(f => f.Id == id).Select(p =>  p.IkProcess.СodeIkProcess + "-" + p.IkProcess.RSMK ).FirstOrDefault());
			

			return View("~/Views/EditAndGenerateReports/GenReport.cshtml", report);

		}
		[HttpPost]
		public async Task<IActionResult> EditReport(string keks, int id)
		{

			var report = _db.UserReports.Where(p => p.Id == id).FirstOrDefault();
			report.RowHtmlCode = keks;
			report.LoadTime = DateTime.Now;
			await _db.SaveChangesAsync();

			return View("~/Views/EditAndGenerateReports/GenReport.cshtml", report);
		}
		[HttpPost]
		public async Task<IActionResult> AddReportAsNew(string keks, int id)
		{

			var report = _db.UserReports.Where(p => p.Id == id).FirstOrDefault();
			await _db.UserReports.AddAsync(new Models.IKProcess.ReportViewModel
			{
				Season = report.Season,
				LoadTime = DateTime.Now,
				IkProcessId = report.IkProcessId,
				RowHtmlCode = keks,
				UserId = _userManager.GetUserAsync(User).Result.Id
			});
			await _db.SaveChangesAsync();

			return View("~/Views/EditAndGenerateReports/GenReport.cshtml", report);
		}



	}
}
