using Microsoft.AspNetCore.Identity;
using SMKMIVLGU.Models;
using System.Threading.Tasks;

namespace SMKMIVLGU
{
	public class UserInitializer
	{
		public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			string admin = "Admin";
			string password = "Admin";
			if (await roleManager.FindByNameAsync("admin") == null)
			{
				await roleManager.CreateAsync(new IdentityRole("admin"));

			}
			if (await roleManager.FindByNameAsync("writer") == null)
			{
				await roleManager.CreateAsync(new IdentityRole("writer"));
			}
			if (await roleManager.FindByNameAsync("reader") == null)
			{
				await roleManager.CreateAsync(new IdentityRole("reader"));
			}
			if (await userManager.FindByNameAsync(admin) == null)
			{
				User adminS = new User { UserName = admin };
				IdentityResult result = await userManager.CreateAsync(adminS, password);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(adminS, "admin");

				}
			}
		}
	}
}
