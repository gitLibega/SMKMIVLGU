using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SMKMIVLGU.Models;
using SMKMIVLGU.Models.IKProcess;

namespace SMKMIVLGU.Controllers.IK_Report
{
	public class IkProcessEditor : Controller
	{
		ApplicationDbContext _db;
		public IkProcessEditor(ApplicationDbContext db)
		{
			_db = db;
		}


		[HttpGet]
		public IActionResult EditIkProcess(int id = 0)
		{
			ViewBag.Iks = _db.IkProcesses.ToList();

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
		public async Task<IActionResult> EditIkProcess(string[] ikProcessData, string keks, int id)
		{
			ViewBag.Iks = _db.IkProcesses.ToList();
			ViewBag.ikName = _db.IkProcesses.Where(p => p.id == id).FirstOrDefault();

			var ikProcess = _db.IkProcesses.Where(p => p.id == id).FirstOrDefault();
			ikProcess.СodeIkProcess = ikProcessData[0];
			ikProcess.СodeDocProcedure = ikProcessData[1];
			ikProcess.RSMK = ikProcessData[2];

			var rowId = _db.configurationPatterns.Where(p => p.IkProcessId == id).FirstOrDefault();
			var row = _db.patternIkProcessRows.Where(p => p.Id == rowId.PatternIkProcessRowId).FirstOrDefault();
			row.RowHtmlCode = keks;
			await _db.SaveChangesAsync();

			return RedirectToAction("EditIkProcess", id);
		}
		[HttpGet]
		public IActionResult AddIkProcess()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddIkProcess(string[] ikProcessData, string keks)
		{

			if (ModelState.IsValid)
			{

				IkProcessViewModel model = new IkProcessViewModel
				{ СodeIkProcess = ikProcessData[0], СodeDocProcedure = ikProcessData[1], RSMK = ikProcessData[2] };
				await _db.IkProcesses.AddAsync(model);
				await _db.SaveChangesAsync();
				var rows = new List<PatternIkProcessRowViewModel>();


				await _db.patternIkProcessRows.AddAsync(new PatternIkProcessRowViewModel
				{
					RowHtmlCode = keks

				});



				await _db.SaveChangesAsync();
				var lastId = _db.patternIkProcessRows.OrderByDescending(x => x.Id).FirstOrDefault();
				var id = _db.IkProcesses.OrderByDescending(x => x.id).FirstOrDefault();
				var config = new ConfigurationPatternViewModel { IkProcessId = id.id, PatternIkProcessRowId = lastId.Id };
				await _db.configurationPatterns.AddAsync(config);
				await _db.SaveChangesAsync();
				return RedirectToAction("AddIkProcess");
			}
			else
			{

			}
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> DeleteIkProcess(int id)
		{
			var config = _db.configurationPatterns.Where(p => p.IkProcessId == id).FirstOrDefault();
			_db.IkProcesses.Remove(_db.IkProcesses.Find(id));
			_db.patternIkProcessRows.Remove(_db.patternIkProcessRows.Find(config.Id));
			await _db.SaveChangesAsync();
			return RedirectToAction("EditIkProcess", 0);
		}




	}
}
 