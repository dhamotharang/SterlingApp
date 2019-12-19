using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Models
{
    public class MaturityParam
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
       public int Stage { get; set; }
       public string RefNo { get; set; }
       public string Rating { get; set; }
       public string Notes { get; set; }
    }
}