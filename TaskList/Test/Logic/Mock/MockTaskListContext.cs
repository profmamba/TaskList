using FakeDbSet;
using FakeItEasy;
using ProfMamba.TaskList.Interfaces;
using ProfMamba.TaskList.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace ProfMamba.TaskList.Test.Logic.Mock
{
	public class MockTaskListContext : ITaskListContext
	{
		//Properties

		public IDbSet<Task> Tasks { get; set; }

		//Constructors

		public MockTaskListContext()
		{
			Tasks = new InMemoryDbSet<Task>(true);

			Populate();
		}

		//Methods

		private void Populate()
		{
			Tasks.Add(new Task()
			{
				TaskId = 1,
				CreateDate = DateTime.Now,
				Deleted = false,
				Description = "Test Task 1",
				TaskType = TaskType.Work
			});

			Tasks.Add(new Task()
			{
				TaskId = 2,
				CreateDate = DateTime.Now,
				Deleted = false,
				Description = "Test Task 2",
				TaskType = TaskType.ScheduledMeeting
			});

			Tasks.Add(new Task()
			{
				TaskId = 3,
				CreateDate = DateTime.Now,
				Deleted = false,
				Description = "Test Task 3",
				TaskType = TaskType.Social
			});

			Tasks.Add(new Task()
			{
				TaskId = 4,
				CreateDate = DateTime.Now,
				Deleted = true,
				Description = "Test Task 4",
				TaskType = TaskType.Work
			});

		}

		public int SaveChanges()
		{
			return 0;
		}

		public System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
		{
			return A.Fake<DbEntityEntry<TEntity>>();
		}

		public void Dispose()
		{
			if (Tasks != null)
				Tasks = null;
		}
	}
}
