using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProfMamba.TaskList.Objects;

namespace ProfMamba.TaskList.Interfaces
{
	public interface ITaskListLogic
	{
		IEnumerable<Task> GetTasks();
		void UpsertTask(Task task);
		void DeleteTask(int taskId);
	}
}
