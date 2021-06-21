using System;
using System.ComponentModel.DataAnnotations;

namespace SMKMIVLGU.Models.IKProcess
{
	public class ReportViewModel
	{
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }
		public string RowHtmlCode { get; set; }

		public int IkProcessId { get; set; }
		public IkProcessViewModel IkProcess { get; set; }
		public string Season { get; set; }
		public DateTime LoadTime { get; set; }
		public int AverageCoef { get; set; }
	}
}
