using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfMamba.TaskList.Interfaces
{
	public interface ITaskListContextFactory
	{
		ITaskListContext Create();
	}
}
