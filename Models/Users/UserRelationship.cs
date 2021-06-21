using System.ComponentModel.DataAnnotations;

namespace SMKMIVLGU.Models
{
	public class UserRelationship
	{
		[Key]
		public int id { get; set; }
		public string idUser { get; set; }
		public User User { get; set; }
		public string idWard { get; set; }

	}
}
