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

namespace CoreLoggerDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogTrace(LoggingEvents.ViewHomepage,
                    "Viewed homepage at {DATE_TIME}.",
                    DateTime.Now);

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
