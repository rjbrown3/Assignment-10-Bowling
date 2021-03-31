using BowlingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingApp.Components
{
    public class TeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;

        public TeamViewComponent(BowlingLeagueContext con)
        {
            context = con;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["team"];   //store what team the user selects in the viewbag to be displayed in Default.cshtml

            return View(context.Teams
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
