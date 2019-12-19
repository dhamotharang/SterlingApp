using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Fintrak.Client.Basic.Entities;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;

namespace Fintrak.Client.Basic.Contracts
{
    [ServiceContract]
    public interface IIFRSCoreService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();


        #region IFRSRegistry

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IFRSRegistry UpdateIFRSRegistry(IFRSRegistry ifrsRegistry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIFRSRegistry(int ifrsRegistryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IFRSRegistry GetIFRSRegistry(int ifrsRegistryId);

        [OperationContract]
        IFRSRegistryData[] GetAllIFRSRegistries();

        #endregion

        #region DerivedCaption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        DerivedCaption UpdateDerivedCaption(DerivedCaption derivedCaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteDerivedCaption(int derivedCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        DerivedCaption GetDerivedCaption(int derivedCaptionId);

        [OperationContract]
        DerivedCaption[] GetAllDerivedCaptions();

        #endregion

       

    }
}
