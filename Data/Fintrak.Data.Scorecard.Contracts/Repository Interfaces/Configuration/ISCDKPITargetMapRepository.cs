using Fintrak.Shared.Scorecard.Entities;
using Fintrak.Shared.Scorecard.Framework;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.Scorecard.Contracts
{
    public interface ISCDKPITargetMapRepository : IDataRepository<SCDKPITargetMap>
    {
        IEnumerable<SCDKPITargetMapInfo> GetSCDKPITargetMaps();
    }
}
