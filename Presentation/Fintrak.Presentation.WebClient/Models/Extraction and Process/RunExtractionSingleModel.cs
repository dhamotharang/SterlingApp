//using Fintrak.Client.MPR.Contracts;
//using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class RunExtractionSingleModel
    {
        public int SolutionId { get; set; }

        public string SolutionName { get; set; }

        public int ExtractionId { get; set; }

        public string ExtrationTitle { get; set; }

        public bool CanRun { get; set; }
    }
}