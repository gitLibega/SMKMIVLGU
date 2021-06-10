using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMKMIVLGU.Models
{
	public class PatternIkProcessRowViewModel
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string RowHtmlCode { get; set; }
		
	}
}
