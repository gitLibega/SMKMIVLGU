using System.ComponentModel.DataAnnotations;

namespace SMKMIVLGU.Models
{
	public class NewRoleViewModel
	{
		[Required(ErrorMessage = "Поле должно быть установлено")]
		public string nameRole { get; set; }
	}
}
