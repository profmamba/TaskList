using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfMamba.TaskList.WebApp.Models
{
	public class ApiResult
	{
		//Properties

		public bool success { get; set; }
		public string message { get; set; }
		public object data { get; set; }

		//Methods

		public void SetSavedSuccess(string typeName, object data)
		{
			this.success = true;
			this.message = string.Format("Your {0} was saved.", typeName);
			this.data = data;
		}

		public void SetSavedFailed(string typeName)
		{
			this.message = string.Format("Could not save the {0}. Please try again later.", typeName);
		}

		public void SetDeleteSuccess(string typeName)
		{
			this.success = true;
			this.message = string.Format("The {0} was deleted.", typeName);
		}

		public void SetDeleteFailed(string typeName)
		{
			this.message = string.Format("Could not delete the {0}. Please try again later.", typeName);
		}
	}
}