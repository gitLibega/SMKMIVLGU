using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
