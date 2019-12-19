//using Fintrak.Client.MPR.Contracts;
//using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class SelectionExtractionModel
    {
        public ExtractionData Extraction { get; set; }

        public bool CanRun { get; set; }
    }
}