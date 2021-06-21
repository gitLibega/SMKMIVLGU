using System.ComponentModel.DataAnnotations;

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
