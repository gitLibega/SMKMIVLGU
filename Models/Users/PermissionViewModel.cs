using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SMKMIVLGU.Models
{
	public class PermissionViewModel
	{
		public string UserId { get; set; }
		public string UserName { get; set; }

		public List<IdentityRole> AllRoles { get; set; }
		public IList<string> UserRoles { get; set; }
		public PermissionViewModel()
		{
			AllRoles = new List<IdentityRole>();
			UserRoles = new List<string>();
		}
	}
}
