using System.Collections.Generic;
using DevExpress.DataAccess.Sql;
using Microsoft.AspNetCore.Mvc;

namespace devexpress_universal_reporting.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult Error() {
            Models.ErrorModel model = new Models.ErrorModel();
            return View(model);
        }

        public IActionResult Designer() {
            Models.ReportDesignerModel model = new Models.ReportDesignerModel();
            // Create a SQL data source with the specified connection string.
            //SqlDataSource ds = new SqlDataSource("NWindConnectionString");
            // Create a SQL query to access the Products data table.
           // SelectQuery query = SelectQueryFluentBuilder.AddTable("Products").SelectAllColumnsFromTable().Build("Products");
           // ds.Queries.Add(query);
           // ds.RebuildResultSchema();
           // model.DataSources = new Dictionary<string, object>();
           // model.DataSources.Add("Northwind", ds);
            return View(model);
        }        
        public IActionResult Viewer(string ReportName)
        {
            ViewBag.ReportName = ReportName;
            return View();
        }
    }
}