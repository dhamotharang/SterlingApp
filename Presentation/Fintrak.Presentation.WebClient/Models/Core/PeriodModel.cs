using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class PeriodModel
    {
        public FiscalYear FiscalYear { get; set; }
        public FiscalPeriodData[] FiscalPeriods { get; set; }
    }
}