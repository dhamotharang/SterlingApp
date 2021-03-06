using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ICostCentreRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CostCentreRepository : DataRepositoryBase<CostCentre>, ICostCentreRepository
    {

        protected override CostCentre AddEntity(BasicContext entityContext, CostCentre entity)
        {
            return entityContext.Set<CostCentre>().Add(entity);
        }

        protected override CostCentre UpdateEntity(BasicContext entityContext, CostCentre entity)
        {
            return (from e in entityContext.Set<CostCentre>() 
                    where e.CostCentreId == entity.CostCentreId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CostCentre> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<CostCentre>()
                   select e;
        }

        protected override CostCentre GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CostCentre>()
                         where e.CostCentreId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<CostCentreInfo> GetCostCentres()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.CostCentreSet
                            join b in entityContext.CostCentreDefinitionSet on a.DefinitionCode equals b.Code
                            join c in entityContext.CostCentreSet on a.Parent equals c.Code into parents
                            from pt in parents.DefaultIfEmpty()
                            select new CostCentreInfo()
                            {
                                CostCentre = a,
                                CostCentreDefinition = b,
                                Parent = pt
                            };

                return query.ToFullyLoaded();
            }
        }


    }
}
