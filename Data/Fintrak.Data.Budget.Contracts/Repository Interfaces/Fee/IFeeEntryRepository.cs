using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IFeeEntryRepository : IDataRepository<FeeEntry>
    {
        IEnumerable<FeeEntryInfo> GetFeeEntries(string year, string reviewCode,string definitionCode,string misCode);
        IEnumerable<FeeEntryInfo> GetFeeEntries(string year, string reviewCode);
    }
}
