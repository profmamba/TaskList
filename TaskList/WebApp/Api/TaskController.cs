using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProfMamba.TaskList.Interfaces;
using ProfMamba.TaskList.Objects;
using ProfMamba.TaskList.WebApp.Filters;
using ProfMamba.TaskList.WebApp.Helpers;
using ProfMamba.TaskList.WebApp.Models;

namespace ProfMamba.TaskList.WebApp.Api
{
	public class TaskController : ApiController
	{
		//Fields
		
		private readonly ITaskListLogic logic;

		//Constructors

		public TaskController(ITaskListLogic logic)
		{
			this.logic = logic;
		}

		//Methods

		public IEnumerable<TaskView> Get()
		{
			return logic.GetTasks().Select(t => new TaskView(t));
		}

		[ValidateModel]
		public HttpResponseMessage Post(Task task)
		{
			var result = new ApiResult();

			try
			{
				logic.UpsertTask(task);

				result.SetSavedSuccess("task", new TaskView(task));
			}
			catch
			{
				result.SetSavedFailed("task");

				//TODO: Log failure and exception
			}
			
			return Request.CreateResponse(result);
		}

		[ValidateModel]
		public HttpResponseMessage Put(Task task)
		{
			//follows upsert semantic > put only exists for RESTful correctness
			return Post(task);
		}

		public HttpResponseMessage Delete(int id)
		{
			var result = new ApiResult();
			try
			{
				logic.DeleteTask(id);

				result.SetDeleteSuccess("task");
			}
			catch
			{
				result.SetDeleteFailed("task");

				//TODO: Log failure and exception
			}

			return Request.CreateResponse(result);
		}
	}
}