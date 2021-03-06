using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class CapexCostInfo
    {
        public CapexCost CapexCost { get; set; }
        public CapexItem CapexItem { get; set; }
        public CapexCategory CapexCategory { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
        public Team Team { get; set; }
        public Currency Currency { get; set; }
    }
}