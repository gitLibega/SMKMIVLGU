using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMKMIVLGU.Models
{
	public class NewRoleViewModel
	{
		[Required(ErrorMessage = "Поле должно быть установлено")]
		public string nameRole { get; set; }
	}
}
