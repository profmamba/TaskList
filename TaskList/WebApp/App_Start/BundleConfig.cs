using System.Web;
using System.Web.Optimization;

namespace ProfMamba.TaskList.WebApp
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/core").Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/bootstrap.js",
						"~/Scripts/jquery.timeago.js",
						"~/Scripts/bootbox.js",
						"~/Scripts/bootstrap-datetimepicker.min.js",
						"~/Scripts/knockout-{version}.js",
						"~/Scripts/knockout.mapping-latest.js",
						"~/Scripts/knockout/shared.ko.js",
						"~/Scripts/knockout/bindings.ko.js"));

			bundles.Add(new ScriptBundle("~/bundles/ie").Include(
						"~/Scripts/html5shiv.js",
						"~/Scripts/html5shiv-printshiv.js",
						"~/Scripts/respond.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
						"~/Content/bootstrap/bootstrap.css",
						"~/Content/bootstrap/bootstrap-theme.css",
						"~/Content/bootstrap/bootstrap-datetimepicker.min.css",
						"~/Content/site.css"));
		}
	}
}