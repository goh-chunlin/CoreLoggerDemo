using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreLoggerDemo.Models;
using CoreLoggerDemo.Models.DataEntryViewModels;
using Microsoft.Extensions.Logging;
using CoreLoggerDemo.Constants;
using System.Text;
using System.IO;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights;

namespace CoreLoggerDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        private TelemetryClient telemetryClient = new TelemetryClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogTrace(LoggingEvents.ViewHomepage,
                    "Viewed homepage at {DATE_TIME}.",
                    DateTime.Now);

            var visitor = new MetricTelemetry();
            visitor.Name = "Visitor";
            visitor.Sum += 1;
            telemetryClient.TrackMetric(visitor);

            telemetryClient.TrackTrace("Visiting Homepage",
               SeverityLevel.Information,
               new Dictionary<string, string> { { "Date and Time of Visit", DateTime.Now.ToString("yyyy-MM-dd HH:mm") } });

            try
            {
                using (var streamReader = new StreamReader(@"\testing.txt", Encoding.UTF8))
                {
                    string output = await streamReader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "Read testing.txt file at homepage at {DATE_TIME}.", DateTime.Now);
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetDataEntry(DataEntryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation(
                    LoggingEvents.CreateNewMessage, 
                    "Submitted message {MESSAGE} by {PERSON} with the email {EMAIL}.", 
                    model.Message, model.CreatedBy, model.Email);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
