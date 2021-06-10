using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMKMIVLGU.Models.IKProcess
{
	public class UserIkProcessRelViewModel
	{ 
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		public int IkProcessid { get; set; }

		public User User { get; set; }
		public IkProcessViewModel IkProcess { get; set; }
		
		

	}
}
