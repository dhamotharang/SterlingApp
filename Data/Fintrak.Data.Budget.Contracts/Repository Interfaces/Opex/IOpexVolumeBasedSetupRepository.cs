using Fintrak.Shared.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Budget.Contracts
{
    public interface IOpexVolumeBasedSetupRepository : IDataRepository<OpexVolumeBasedSetup>
    {
        IEnumerable<OpexVolumeBasedSetupInfo> GetOpexVolumeBasedSetups(string year, string reviewCode,string itemCode);
        IEnumerable<OpexVolumeBasedSetupInfo> GetOpexVolumeBasedSetups(string year, string reviewCode);
    }
}
