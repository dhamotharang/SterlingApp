using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IManagementTreeRepository : IDataRepository<ManagementTree>
    {
        IEnumerable<ManagementTreeInfo> GetManagementTrees(string curYr);
    }
}
