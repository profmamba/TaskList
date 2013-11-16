using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProfMamba.TaskList.Interfaces;
using ProfMamba.TaskList.Objects;

namespace ProfMamba.TaskList.Logic
{
	public class TaskListLogic : ITaskListLogic
	{
		//Fields

		private readonly ITaskListContextFactory contextFactory;

		//Constructors

		public TaskListLogic(ITaskListContextFactory contextFactory)
		{
			this.contextFactory = contextFactory;
		}

		//Methods

		public IEnumerable<Task> GetTasks()
		{
			using (var ctx = contextFactory.Create())
			{
				return ctx.Tasks.Where(t => !t.Deleted).OrderBy(t => t.Description).ToList();
			}
		}

		public void UpsertTask(Task task)
		{
			using (var ctx = contextFactory.Create())
			{
				if (task.TaskId == 0)
				{
					task.CreateDate = DateTime.Now;
					ctx.Tasks.Add(task);
				}
				else
				{
					var currentTask = ctx.Tasks.SingleOrDefault(t => t.TaskId == task.TaskId);

					if (currentTask == null)
						throw new RecordNotFoundException<Task>(task.TaskId);
					
					//'last in wins' concurrency
					currentTask.Description = task.Description;
					currentTask.TaskType = task.TaskType;
				}

				ctx.SaveChanges();
			}
		}

		public void DeleteTask(int taskId)
		{
			using (var ctx = contextFactory.Create())
			{
				var task = ctx.Tasks.SingleOrDefault(t => t.TaskId == taskId);

				if (task == null)
					throw new RecordNotFoundException<Task>(taskId);

				task.Deleted = true;

				ctx.SaveChanges();
			}
		}
	}
}
