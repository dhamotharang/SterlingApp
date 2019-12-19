using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Client.Budget.Entities;

namespace Fintrak.Client.Budget.Contracts
{
    [ServiceContract]
    public interface IStaffCostService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region Grade

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Grade UpdateGrade(Grade grade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGrade(int gradeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Grade GetGrade(int gradeId);

        [OperationContract]
        Grade[] GetAllGrades();

        #endregion

        #region PayClassification

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PayClassification UpdatePayClassification(PayClassification payClassification);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePayClassification(int payClassificationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PayClassification GetPayClassification(int payClassificationId);

        [OperationContract]
        PayClassification[] GetAllPayClassifications();

        #endregion

        #region PayStructure

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PayStructure UpdatePayStructure(PayStructure payStructure);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePayStructure(int payStructureId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PayStructure GetPayStructure(int payStructureId);

        [OperationContract]
        PayStructure[] GetAllPayStructures();

        #endregion

        #region StaffCost

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        StaffCost UpdateStaffCost(StaffCost staffCost);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteStaffCost(int staffCostId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        StaffCost GetStaffCost(int staffCostId);

        [OperationContract]
        StaffCost[] GetAllStaffCosts();

        #endregion

        #region StaffCount

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        StaffCount UpdateStaffCount(StaffCount staffCount);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteStaffCount(int staffCountId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        StaffCount GetStaffCount(int staffCountId);

        [OperationContract]
        StaffCount[] GetAllStaffCounts();

        #endregion


    

    }
}
