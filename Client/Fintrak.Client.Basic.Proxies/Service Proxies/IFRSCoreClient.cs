using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Basic.Contracts;
using Fintrak.Client.Basic.Entities;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Basic.Proxies
{
    [Export(typeof(IIFRSCoreService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSCoreClient : UserClientBase<IIFRSCoreService>, IIFRSCoreService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region IFRSRegistry

        public IFRSRegistry UpdateIFRSRegistry(IFRSRegistry ifrsRegistry)
        {
            return Channel.UpdateIFRSRegistry(ifrsRegistry);
        }

        public void DeleteIFRSRegistry(int ifrsRegistryId)
        {
            Channel.DeleteIFRSRegistry(ifrsRegistryId);
        }

        public IFRSRegistry GetIFRSRegistry(int ifrsRegistryId)
        {
            return Channel.GetIFRSRegistry(ifrsRegistryId);
        }

        public IFRSRegistryData[] GetAllIFRSRegistries()
        {
            return Channel.GetAllIFRSRegistries();
        }

       

        #endregion

        #region DerivedCaption

        public DerivedCaption UpdateDerivedCaption(DerivedCaption derivedCaption)
        {
            return Channel.UpdateDerivedCaption(derivedCaption);
        }

        public void DeleteDerivedCaption(int derivedCaptionId)
        {
            Channel.DeleteDerivedCaption(derivedCaptionId);
        }

        public DerivedCaption GetDerivedCaption(int derivedCaptionId)
        {
            return Channel.GetDerivedCaption(derivedCaptionId);
        }

        public DerivedCaption[] GetAllDerivedCaptions()
        {
            return Channel.GetAllDerivedCaptions();
        }


        #endregion

    }
}
