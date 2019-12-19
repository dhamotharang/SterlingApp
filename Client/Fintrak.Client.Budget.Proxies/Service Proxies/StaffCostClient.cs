using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Budget.Contracts;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Budget.Proxies
{
    [Export(typeof(IStaffCostService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StaffCostClient : UserClientBase<IStaffCostService>, IStaffCostService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region Grade

        public Grade UpdateGrade(Grade grade)
        {
            return Channel.UpdateGrade(grade);
        }

        public void DeleteGrade(int gradeId)
        {
           Channel.DeleteGrade(gradeId);
        }

        public Grade GetGrade(int gradeId)
        {
            return Channel.GetGrade(gradeId);
        }

        public Grade[] GetAllGrades()
        {
            return Channel.GetAllGrades();
        }

        #endregion

        #region PayClassification

        public PayClassification UpdatePayClassification(PayClassification payClassification)
        {
            return Channel.UpdatePayClassification(payClassification);
        }

        public void DeletePayClassification(int payClassificationId)
        {
            Channel.DeletePayClassification(payClassificationId);
        }

        public PayClassification GetPayClassification(int payClassificationId)
        {
            return Channel.GetPayClassification(payClassificationId);
        }

        public PayClassification[] GetAllPayClassifications()
        {
            return Channel.GetAllPayClassifications();
        }

        #endregion

        #region PayStructure

        public PayStructure UpdatePayStructure(PayStructure payStructure)
        {
            return Channel.UpdatePayStructure(payStructure);
        }

        public void DeletePayStructure(int payStructureId)
        {
            Channel.DeletePayStructure(payStructureId);
        }

        public PayStructure GetPayStructure(int payStructureId)
        {
            return Channel.GetPayStructure(payStructureId);
        }

        public PayStructure[] GetAllPayStructures()
        {
            return Channel.GetAllPayStructures();
        }

        #endregion

        #region StaffCost

        public StaffCost UpdateStaffCost(StaffCost staffCost)
        {
            return Channel.UpdateStaffCost(staffCost);
        }

        public void DeleteStaffCost(int staffCostId)
        {
            Channel.DeleteStaffCost(staffCostId);
        }

        public StaffCost GetStaffCost(int staffCostId)
        {
            return Channel.GetStaffCost(staffCostId);
        }

        public StaffCost[] GetAllStaffCosts()
        {
            return Channel.GetAllStaffCosts();
        }

        #endregion

        #region StaffCount

        public StaffCount UpdateStaffCount(StaffCount staffCount)
        {
            return Channel.UpdateStaffCount(staffCount);
        }

        public void DeleteStaffCount(int staffCountId)
        {
            Channel.DeleteStaffCount(staffCountId);
        }

        public StaffCount GetStaffCount(int staffCountId)
        {
            return Channel.GetStaffCount(staffCountId);
        }

        public StaffCount[] GetAllStaffCounts()
        {
            return Channel.GetAllStaffCounts();
        }

        #endregion



        

    }
}
