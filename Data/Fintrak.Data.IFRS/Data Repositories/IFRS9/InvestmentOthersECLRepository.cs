using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IInvestmentOthersECLRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InvestmentOthersECLRepository : DataRepositoryBase<InvestmentOthersECL>, IInvestmentOthersECLRepository
    {
        protected override InvestmentOthersECL AddEntity(IFRSContext entityContext, InvestmentOthersECL entity)
        {
            return entityContext.Set<InvestmentOthersECL>().Add(entity);
        }

        protected override InvestmentOthersECL UpdateEntity(IFRSContext entityContext, InvestmentOthersECL entity)
        {
            return (from e in entityContext.Set<InvestmentOthersECL>()
                    where e.ecl_Id == entity.ecl_Id
                    select e).FirstOrDefault();
        }
        protected override IEnumerable<InvestmentOthersECL> GetEntities(IFRSContext entityContext)
        {
            var query = from a in entityContext.InvestmentOthersECLSet
                        select a;

            return query.ToFullyLoaded().OrderBy(a => a.period).GroupBy(a => a.RefNo).Select(a => a.First()).Take(500);
        }

        protected override InvestmentOthersECL GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<InvestmentOthersECL>()
                         where e.ecl_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public IEnumerable<InvestmentOthersECL> GetInvestmentOthersECLByRefNo(string RefNo)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.InvestmentOthersECLSet
                            where a.RefNo.Contains(RefNo)
                            select a;

                return query.ToFullyLoaded();
            }
        }
    }
}