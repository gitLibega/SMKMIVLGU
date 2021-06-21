using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMKMIVLGU.Models.IKProcess;

namespace SMKMIVLGU.Models

{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
				: base(options)
		{
			Database.EnsureCreated();
		}


		//Связи пользователей
		public DbSet<UserRelationship> UserRelationships { get; set; }


		/// <summary>
		/// Ик-процессы
		/// </summary>
		public DbSet<IkProcessViewModel> IkProcesses { get; set; }
		public DbSet<PatternIkProcessRowViewModel> patternIkProcessRows { get; set; }
		public DbSet<UserIkProcessRelViewModel> UserIkProcessRels { get; set; }
		public DbSet<ConfigurationPatternViewModel> configurationPatterns { get; set; }
		public DbSet<ReportViewModel> UserReports { get; set; }
	}
}