using ProfMamba.TaskList.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfMamba.TaskList.WebApp.Models
{
	public class TaskView
	{
		public int TaskId { get; set; }
		public string Description { get; set; }
		public DateTime CreateDate { get; set; }
		public TaskType TaskType { get; set; }
		public string TaskTypeName { get; set; }

		public TaskView(Task task)
		{
			this.TaskId = task.TaskId;
			this.Description = task.Description;
			this.CreateDate = task.CreateDate;
			this.TaskType = task.TaskType;
			this.TaskTypeName = task.TaskType.ToFriendlyString();
		}
	}
}