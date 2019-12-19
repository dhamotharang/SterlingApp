using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class ConsolidatedTrialBalanceGapModel
    {
        public TrialBalanceConsolidated[] TrialBalance { get; set; }
        public decimal TranslatedBalance { get; set; }

        public decimal IFRSAutoBalance { get; set; }
        public decimal OBSAutoBalance { get; set; }
        public decimal BSAutoBalance { get; set; }
    }
}