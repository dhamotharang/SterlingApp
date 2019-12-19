using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.IFRS.Proxies
{
    [Export(typeof(IFinancialInstrumentService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FinancialInstrumentClient : UserClientBase<IFinancialInstrumentService>, IFinancialInstrumentService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }



        #region FairValueBasisMap

        public FairValueBasisMap UpdateFairValueBasisMap(FairValueBasisMap fairValueBasisMap)
        {
            return Channel.UpdateFairValueBasisMap(fairValueBasisMap);
        }

        public void DeleteFairValueBasisMap(int fairValueBasisMapId)
        {
            Channel.DeleteFairValueBasisMap(fairValueBasisMapId);
        }

        public FairValueBasisMap GetFairValueBasisMap(int fairValueBasisMapId)
        {
            return Channel.GetFairValueBasisMap(fairValueBasisMapId);
        }

        public FairValueBasisMapData[] GetAllFairValueBasisMaps()
        {
            return Channel.GetAllFairValueBasisMaps();
        }


        #endregion


        #region FairValueBasisExemption

        public FairValueBasisExemption UpdateFairValueBasisExemption(FairValueBasisExemption fairValueBasisExemption)
        {
            return Channel.UpdateFairValueBasisExemption(fairValueBasisExemption);
        }

        public void DeleteFairValueBasisExemption(int fairValueBasisExemptionId)
        {
            Channel.DeleteFairValueBasisExemption(fairValueBasisExemptionId);
        }

        public FairValueBasisExemption GetFairValueBasisExemption(int fairValueBasisExemptionId)
        {
            return Channel.GetFairValueBasisExemption(fairValueBasisExemptionId);
        }

        public FairValueBasisExemptionData[] GetAllFairValueBasisExemptions()
        {
            return Channel.GetAllFairValueBasisExemptions();
        }


        #endregion

    }
}
