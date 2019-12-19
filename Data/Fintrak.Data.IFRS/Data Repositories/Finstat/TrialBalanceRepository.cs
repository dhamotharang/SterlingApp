using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ITrialBalanceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TrialBalanceRepository : DataRepositoryBase<TrialBalance>, ITrialBalanceRepository
    {

        protected override TrialBalance AddEntity(IFRSContext entityContext, TrialBalance entity)
        {
            return entityContext.Set<TrialBalance>().Add(entity);
        }

        protected override TrialBalance UpdateEntity(IFRSContext entityContext, TrialBalance entity)
        {
            return (from e in entityContext.Set<TrialBalance>() 
                    where e.TrialBalanceId == entity.TrialBalanceId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TrialBalance> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<TrialBalance>()
                   select e;
        }

        protected override TrialBalance GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TrialBalance>()
                         where e.TrialBalanceId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TrialBalance> GetTrialBalances(DateTime runDate)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.TrialBalanceSet
                            where a.TransDate == runDate
                            select a;

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<TrialBalance> GetTrialBalancesByBranch(DateTime runDate, string branchCode)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.TrialBalanceSet
                            where a.TransDate == runDate && a.BranchCode == branchCode
                            select a;

                return query.ToFullyLoaded();
            }
        }
      
    }
}
