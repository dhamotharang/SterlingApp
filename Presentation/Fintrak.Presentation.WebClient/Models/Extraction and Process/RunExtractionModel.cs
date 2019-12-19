//using Fintrak.Client.MPR.Contracts;
//using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class RunExtractionModel
    {
        public KeyValueModel[] Solutions { get; set; }

        public ExtractionData[] Extractions { get; set; }
    }
}