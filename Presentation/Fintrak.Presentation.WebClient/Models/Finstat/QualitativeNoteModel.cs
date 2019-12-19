using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class QualitativeNoteModel
    {

        public string RefNote { get; set; }
        public string report { get; set; }
        public string TopNote { get; set; }
        public string BottomNote { get; set; }
        public DateTime RunDate { get; set; }
        public int ReportType { get; set; }        
    }
}