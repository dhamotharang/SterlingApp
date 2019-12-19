using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class CurrencyModel
    {
        public Currency Currency { get; set; }
        public CurrencyRateData[] CurrencyRates { get; set; }
    }
}