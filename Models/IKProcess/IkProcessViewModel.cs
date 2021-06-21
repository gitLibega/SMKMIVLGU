using System.ComponentModel.DataAnnotations;

namespace SMKMIVLGU.Models
{
	public class IkProcessViewModel
	{
		[Key]
		public int id { get; set; }
		[Required(ErrorMessage = "Поле должно быть установлено")]
		public string СodeIkProcess { get; set; }
		[Required(ErrorMessage = "Поле должно быть установлено")]
		public string СodeDocProcedure { get; set; }
		[Required(ErrorMessage = "Поле должно быть установлено")]
		public string RSMK { get; set; }



	}
}
