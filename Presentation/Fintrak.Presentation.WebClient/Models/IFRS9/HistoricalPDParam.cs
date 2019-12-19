using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Models
{
    public class HistoricalPDParam
    {
        public int ComputationType { get; set; }
        public int CurYear { get; set; }
        public int CurPeriod { get; set; }
        public int PrevYear { get; set; }
        public int PrevPeriod { get; set; }
    }
}