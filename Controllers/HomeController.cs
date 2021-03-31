using BowlingApp.Models;
using BowlingApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext con)
        {
            _logger = logger;
            context = con;
        }

        public IActionResult Index(long? teamid, string teamname, int pageNum = 1)
        {
            int pageSize = 5;   //page will display 5 bowlers at a time

            return View(new IndexViewModel
            { 
                Bowlers = (context.Bowlers      //actual dataset returned
                    .Where(b => b.TeamId == teamid || teamid == null)
                    .OrderBy(b => b.BowlerLastName)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()),

                PageNumberingInfo = new PageNumberingInfo   //paging info
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,

                    //if no team has been selected, then get the full count. Otherwise, only count the 
                    //number from the team that has been selected
                    TotalNumItems = (teamid == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(b => b.TeamId == teamid).Count())
                },

                TeamSelection = teamname    //get only bowlers from the selected team
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
