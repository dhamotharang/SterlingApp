using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IHistoricalClassificationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HistoricalClassificationRepository : DataRepositoryBase<HistoricalClassification>, IHistoricalClassificationRepository
    {
        protected override HistoricalClassification AddEntity(IFRSContext entityContext, HistoricalClassification entity)
        {
            return entityContext.Set<HistoricalClassification>().Add(entity);
        }

        protected override HistoricalClassification UpdateEntity(IFRSContext entityContext, HistoricalClassification entity)
        {
            return (from e in entityContext.Set<HistoricalClassification>()
                    where e.HistoricalClassificationId == entity.HistoricalClassificationId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<HistoricalClassification> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<HistoricalClassification>()
                   select e;
        }

        protected override HistoricalClassification GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<HistoricalClassification>()
                         where e.HistoricalClassificationId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
       
    }
}