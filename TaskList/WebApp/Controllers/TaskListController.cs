using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfMamba.TaskList.WebApp.Controllers
{
	public class TaskListController : Controller
	{
		public ActionResult Index()
		{
			return View("Index");
		}
	}
}
