using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMKMIVLGU.Models
{
	public class RegistrationViewModel
	{
		[Required]
		[Display(Name = "FIO")]
		public string FIO { get; set; }

		[Required]
		[Display(Name = "Login")]
		public string Login { get; set; }


		[Required]

		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Required]
		[Display(Name = "Department")]
		public string Department { get; set; }

		public List<CheckBoxItem> Relationships { get; set; }
		public List<CheckBoxItem> IkProcesses { get; set; }

	}
}
