using Microsoft.AspNetCore.Identity;

namespace SMKMIVLGU.Models

{

	public class User : IdentityUser
	{

		public string FIO { get; set; }

		public string Login { get; set; }
		public string Password { get; set; }

		public string Department { get; set; }
	}
}