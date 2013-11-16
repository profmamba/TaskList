using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using ProfMamba.TaskList.WebApp.Models;

namespace ProfMamba.TaskList.WebApp.Helpers
{
	public static class ApiHelper
	{
		public static HttpResponseMessage CreateResponse(this HttpRequestMessage request, ApiResult result)
		{
			if (result.success)
				return request.CreateResponse(HttpStatusCode.OK, result);
			else
				return request.CreateErrorResponse(HttpStatusCode.InternalServerError, result.message);
		}
	}
}