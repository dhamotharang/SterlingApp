using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class ProductSharedRatioInfo
    {
        public ProductSharedRatio ProductSharedRatio { get; set; }
        public RevenueCaption RevenueCaption { get; set; }
        public Product Product { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
        public Team Team { get; set; }
    }
}