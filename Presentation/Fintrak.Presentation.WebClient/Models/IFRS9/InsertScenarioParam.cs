using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Models
{
    public class InsertScenarioParam
    {
        public string sector { get; set; }
        public string microeconomic { get; set; }
        public int year { get; set; }
        public int types { get; set; }

        public float values { get; set; }
    }
}