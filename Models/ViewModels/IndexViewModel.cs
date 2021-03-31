using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingApp.Models.ViewModels
{
    public class IndexViewModel     
    {
        public List<Bowlers> Bowlers { get; set; } //Index page will display a list of Bowlers
        public PageNumberingInfo PageNumberingInfo { get; set; }
        public string TeamSelection { get; set; }
    }
}
