using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;

namespace Fintrak.Client.IFRS.Contracts
{
    [ServiceContract]
    public interface IFinancialInstrumentService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();



        #region FairValueBasisMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FairValueBasisMap UpdateFairValueBasisMap(FairValueBasisMap fairValueBasisMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFairValueBasisMap(int fairValueBasisMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FairValueBasisMap GetFairValueBasisMap(int fairValueBasisMapId);

        [OperationContract]
        FairValueBasisMapData[] GetAllFairValueBasisMaps();

        #endregion

        #region FairValueBasisExemption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FairValueBasisExemption UpdateFairValueBasisExemption(FairValueBasisExemption fairValueBasisExemption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFairValueBasisExemption(int fairValueBasisExemptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FairValueBasisExemption GetFairValueBasisExemption(int fairValueBasisExemptionId);

        [OperationContract]
        FairValueBasisExemptionData[] GetAllFairValueBasisExemptions();

        #endregion

       

    }
}
