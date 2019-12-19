using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class TrialBalanceGapModel
    {
        public TrialBalanceGap[] TrialBalance { get; set; }
        public decimal TranslatedBalance { get; set; }

        public decimal BSAutoBalance;
        public decimal OBSAutoBalance    ;
        public decimal IFRSAutoBalance ;
    }
}