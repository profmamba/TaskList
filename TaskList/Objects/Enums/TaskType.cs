using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfMamba.TaskList.Objects
{
	public enum TaskType
	{
		Work = 1,
		ScheduledMeeting = 2,
		Social = 3
	}

	public static class TaskTypeExtensions
	{
		public static string ToFriendlyString(this TaskType taskType)
		{
			switch (taskType)
			{
				case TaskType.ScheduledMeeting:
					return "Scheduled Meeting";
				default:
					return taskType.ToString();
			}
		}
	}
}
