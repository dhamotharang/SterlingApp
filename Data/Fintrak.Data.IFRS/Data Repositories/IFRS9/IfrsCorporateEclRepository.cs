using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsCorporateEclRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsCorporateEclRepository : DataRepositoryBase<IfrsCorporateEcl>, IIfrsCorporateEclRepository
    {
        protected override IfrsCorporateEcl AddEntity(IFRSContext entityContext, IfrsCorporateEcl entity)
        {
            return entityContext.Set<IfrsCorporateEcl>().Add(entity);
        }

        protected override IfrsCorporateEcl UpdateEntity(IFRSContext entityContext, IfrsCorporateEcl entity)
        {
            return (from e in entityContext.Set<IfrsCorporateEcl>()
                    where e.ecl_id == entity.ecl_id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<IfrsCorporateEcl> GetEntities(IFRSContext entityContext)
        {
            var query = from e in entityContext.Set<IfrsCorporateEcl>()
                        select e;
            query = query.OrderBy(a => a.period).GroupBy(e => e.refno).Select(a => a.FirstOrDefault()).Take(500);
            return query;
        }

        protected override IfrsCorporateEcl GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IfrsCorporateEcl>()
                         where e.ecl_id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<IfrsCorporateEcl> GetEntityByRefNo(string refNo)
        {

            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.IfrsCorporateEclSet
                            where a.refno == refNo
                            select a;

                return query.ToFullyLoaded();
            }
        }
       
    }
}