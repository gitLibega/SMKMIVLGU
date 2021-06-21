using System.ComponentModel.DataAnnotations;

namespace SMKMIVLGU.Models
{
	public class AuthorizationViewModel
	{
		[Required]
		[Display(Name = "Логин")]
		public string Login { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }
		public string ReturnUrl { get; set; }
	}
}
