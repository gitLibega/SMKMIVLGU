using System.ComponentModel.DataAnnotations;

namespace SMKMIVLGU.Models
{
	public class ConfigurationPatternViewModel
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int IkProcessId { get; set; }
		public IkProcessViewModel IkProcess { get; set; }
		[Required]
		public int PatternIkProcessRowId { get; set; }
		public PatternIkProcessRowViewModel PatternIkProcessRow { get; set; }

	}
}
