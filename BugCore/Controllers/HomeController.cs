using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BugCore.Models;
using DataLibrary;
using DataLibrary.BusinessLogic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BugCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CreateBug()
        {
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBug(Bug bug)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                BugProcessor.CreateBug(userId, bug.Name, bug.Description, bug.BugSeverity.ToString(), bug.Status);
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult DeleteBug(int id)
        {
            BugProcessor.DeleteBug(id);
            return RedirectToAction("ShowBugs");
        }

        public IActionResult ShowBugs()
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                List<DataLibrary.Models.Bug> bugs = BugProcessor.LoadBugs(userId);

                List<Bug> postBugs = new List<Bug>();

                foreach (DataLibrary.Models.Bug bug in bugs)
                {
                    postBugs.Add(new Bug
                    {
                        Id = bug.Id,
                        Name = bug.Name,
                        Description = bug.Description,
                        Severity = bug.BugSeverity,
                        Status = bug.Status
                    });
                }
                return View(postBugs);
            }

            return RedirectToAction("Index");
        }
    }
}
