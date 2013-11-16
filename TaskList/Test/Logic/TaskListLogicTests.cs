using System;
using System.Data.Entity;
using System.Linq;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfMamba.TaskList.Interfaces;
using ProfMamba.TaskList.Logic;
using ProfMamba.TaskList.Objects;
using ProfMamba.TaskList.Test.Logic.Mock;

namespace ProfMamba.TaskList.Test.Logic
{
	[TestClass]
	public class TaskListLogicTests
	{
		private ITaskListContextFactory contextFactory;
		private ITaskListContext context;
		private IDbSet<Task> tasks;

		private TaskListLogic CreateLogic()
		{
			contextFactory = A.Fake<ITaskListContextFactory>();
			context = new MockTaskListContext();
			tasks = context.Tasks;

			A.CallTo(() => contextFactory.Create())
				.Returns(context);

			return new TaskListLogic(contextFactory);
		}

		[TestMethod]
		public void GetTasksShouldReturnAllUndeletedTasks()
		{
			//arrange
			var sut = CreateLogic();
			var expected = context.Tasks.Count(t => !t.Deleted);

			//act
			var result = sut.GetTasks();

			//assert
			Assert.AreEqual(expected, result.Count());
		}

		[TestMethod]
		public void UpsertTaskShouldAddNewTask()
		{
			//arrange
			var sut = CreateLogic();
			var ctx = A.Fake<ITaskListContext>();

			A.CallTo(() => contextFactory.Create())
				.Returns(ctx);

			var task = new Task()
			{
				Description = "New Test Task",
				TaskType = TaskType.Social
			};

			A.CallTo(() => ctx.Tasks.Add(task))
				.Invokes(call =>
				{
					(call.Arguments.First() as Task).TaskId = 5;
				});

			//act
			sut.UpsertTask(task);

			//assert
			Assert.AreEqual(5, task.TaskId);
		}

		[TestMethod]
		public void UpsertTaskShouldUpdateExistingTask()
		{
			//arrange
			var sut = CreateLogic();

			var task = new Task()
			{
				TaskId = 1,
				Description = "Test Task 1 Update",
				TaskType = TaskType.Social
			};

			//act
			sut.UpsertTask(task);

			//assert
			Assert.AreEqual(task.Description, tasks.Single(t => t.TaskId == 1).Description);
		}

		[TestMethod]
		[ExpectedException(typeof(RecordNotFoundException<Task>))]
		public void UpsertTaskShouldThrowWithNotFoundTask()
		{
			//arrange
			var sut = CreateLogic();

			var task = new Task()
			{
				TaskId = 55,
				Description = "Test Task 1 Update",
				TaskType = TaskType.Social
			};

			//act
			sut.UpsertTask(task);

		}

		[TestMethod]
		public void DeleteTaskShouldSetDelete()
		{
			//arrange
			var sut = CreateLogic();
			var taskId = tasks.First(t => !t.Deleted).TaskId;

			//act
			sut.DeleteTask(taskId);

			//assert
			Assert.AreEqual(true, tasks.Single(t => t.TaskId == taskId).Deleted);
		}
	}
}
