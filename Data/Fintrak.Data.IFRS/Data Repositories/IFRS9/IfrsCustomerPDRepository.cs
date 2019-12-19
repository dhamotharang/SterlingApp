using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsCustomerPDRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsCustomerPDRepository : DataRepositoryBase<IfrsCustomerPD>, IIfrsCustomerPDRepository
    {
        protected override IfrsCustomerPD AddEntity(IFRSContext entityContext, IfrsCustomerPD entity)
        {
            return entityContext.Set<IfrsCustomerPD>().Add(entity);
        }

        protected override IfrsCustomerPD UpdateEntity(IFRSContext entityContext, IfrsCustomerPD entity)
        {
            return (from e in entityContext.Set<IfrsCustomerPD>()
                    where e.CustomerPDId == entity.CustomerPDId
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsCustomerPD> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsCustomerPD>()
                   select e;
        }

        protected override IfrsCustomerPD GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsCustomerPD>()
                         where e.CustomerPDId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<IfrsCustomerPD> GetEntityByRating(string rating)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.IfrsCustomerPDSet
                            where a.Rating == rating
                            select a;

                return query.ToFullyLoaded();
            }
        }
       
    }
}