using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ProfMamba.TaskList.Objects
{
	public class Task
	{
		public int TaskId { get; set; }
		public TaskType TaskType { get; set; }
		[Required]
		public string Description { get; set; }
		public DateTime CreateDate { get; set; }
		public bool Deleted { get; set; }
	}
}
