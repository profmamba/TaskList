using ProfMamba.TaskList.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfMamba.TaskList.WebApp.Models
{
	public class TaskView
	{
		public int taskId { get; set; }
		public string description { get; set; }
		public DateTime createDate { get; set; }
		public TaskType taskType { get; set; }
		public string taskTypeName { get; set; }

		public TaskView(Task task)
		{
			this.taskId = task.TaskId;
			this.description = task.Description;
			this.createDate = task.CreateDate;
			this.taskType = task.TaskType;
			this.taskTypeName = task.TaskType.ToFriendlyString();
		}
	}
}