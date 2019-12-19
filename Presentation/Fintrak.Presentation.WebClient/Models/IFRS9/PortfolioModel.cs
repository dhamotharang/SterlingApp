
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class PortfolioModel
    {
        public PortfolioExposure[] PortfolioExposure { get; set; }

        public SectorialExposure[] SectorialExposure { get; set; }
        public string PortfolioExposureChart { get; set; }

        public string SectorialExposureChart { get; set; }

        public SummaryReport[] SummaryReport { get; set; }

        public string SummaryReportChart { get; set; }

        public BucketExposure[] BucketExposure { get; set; }
        public string BucketExposureChart { get; set; }

    }
}