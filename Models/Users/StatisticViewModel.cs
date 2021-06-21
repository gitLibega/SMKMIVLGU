using System.Collections.Generic;

namespace SMKMIVLGU.Models.Users
{
	public class StatisticViewModel
	{
		public string CodeIk { get; set; }
		public string RSMK { get; set; }
		public string departament { get; set; }
		public string Date { get; set; }
		public int AverageCoef { get; set; }
		public string Period { get; set; }
		public List<reportStatisticViewModel> listItems { get; set; }
		public StatisticViewModel()
		{
			listItems = new List<reportStatisticViewModel>();
		}

	}
	public class reportStatisticViewModel
	{
		public string CodeIk { get; set; }
		public string RSMK { get; set; }
		public int AverageCoef { get; set; }
	}

	}
