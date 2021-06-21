using System;
using System.Collections.Generic;

namespace SMKMIVLGU.Models
{
	public class EditUserViewModel
	{
		public string idUser { get; set; }

		public String FIO { get; set; }

		public String Login { get; set; }
		public String Password { get; set; }
		public string Department { get; set; }

		public List<User> AllUsers { get; set; }
		public List<string> UserRelationships { get; set; }
		public EditUserViewModel()
		{
			AllUsers = new List<User>();
			UserRelationships = new List<string>();
			AllIks = new List<IkProcessViewModel>();
			UserIks = new List<int>();
		}
		public List<IkProcessViewModel> AllIks { get; set; }
		public List<int> UserIks { get; set; }

	}
}
