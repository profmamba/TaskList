using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfMamba.TaskList.Interfaces;

namespace ProfMamba.TaskList.Context
{
	public class TaskListContextFactory : ITaskListContextFactory
	{
		public ITaskListContext Create()
		{
			return new TaskListContext();
		}
	}
}
