using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IMacrovariableEstimateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MacrovariableEstimateRepository : DataRepositoryBase<MacrovariableEstimate>, IMacrovariableEstimateRepository
    {
        protected override MacrovariableEstimate AddEntity(IFRSContext entityContext, MacrovariableEstimate entity)
        {
            return entityContext.Set<MacrovariableEstimate>().Add(entity);
        }

        protected override MacrovariableEstimate UpdateEntity(IFRSContext entityContext, MacrovariableEstimate entity)
        {
            return (from e in entityContext.Set<MacrovariableEstimate>()
                    where e.MacrovariableEstimate_Id == entity.MacrovariableEstimate_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MacrovariableEstimate> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MacrovariableEstimate>()
                   select e;
        }

        protected override MacrovariableEstimate GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MacrovariableEstimate>()
                         where e.MacrovariableEstimate_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MacrovariableEstimate> GetMacrovariableEstimateByCategory(string Category)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<MacrovariableEstimate>()
                             where e.Category == Category
                             select e);

                return query.ToArray();
            }
        }
       
    }
}