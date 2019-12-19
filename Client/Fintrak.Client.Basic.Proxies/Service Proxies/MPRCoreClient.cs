using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Basic.Contracts;
using Fintrak.Client.Basic.Entities;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Basic.Proxies
{
    [Export(typeof(IMPRCoreService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRCoreClient : UserClientBase<IMPRCoreService>, IMPRCoreService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region UserMIS

        public UserMIS UpdateUserMIS(UserMIS userMIS)
        {
            return Channel.UpdateUserMIS(userMIS);
        }

        public void DeleteUserMIS(int userMISId)
        {
            Channel.DeleteUserMIS(userMISId);
        }

        public UserMIS GetUserMIS(int userMISId)
        {
            return Channel.GetUserMIS(userMISId);
        }

        public UserMIS GetUserMISByLoginID(string loginID)
        {
            return Channel.GetUserMISByLoginID(loginID);
        }

        public UserMIS[] GetAllUserMISs()
        {
            return Channel.GetAllUserMISs();
        }



        #endregion

        #region TeamDefinition

        public TeamDefinition UpdateTeamDefinition(TeamDefinition teamDefinition)
        {
            return Channel.UpdateTeamDefinition(teamDefinition);
        }

        public void DeleteTeamDefinition(int teamDefinitionId)
        {
            Channel.DeleteTeamDefinition(teamDefinitionId);
        }

        public TeamDefinition GetTeamDefinition(int teamDefinitionId)
        {
            return Channel.GetTeamDefinition(teamDefinitionId);
        }

        public TeamDefinition GetTeamDefinitionByCode(string code)
        {
            return Channel.GetTeamDefinitionByCode(code);
        }

        public TeamDefinition[] GetAllTeamDefinitions()
        {
            return Channel.GetAllTeamDefinitions();
        }



        #endregion

        #region AccountMIS

        public AccountMIS UpdateAccountMIS(AccountMIS accountMIS)
        {
            return Channel.UpdateAccountMIS(accountMIS);
        }

        public void DeleteAccountMIS(int accountMISId)
        {
            Channel.DeleteAccountMIS(accountMISId);
        }

        public AccountMIS GetAccountMIS(int accountMISId)
        {
            return Channel.GetAccountMIS(accountMISId);
        }

        public AccountMISData[] GetAllAccountMISs()
        {
            return Channel.GetAllAccountMISs();
        }


        #endregion

        #region AccountOfficerDetail

        public AccountOfficerDetail UpdateAccountOfficerDetail(AccountOfficerDetail accountOfficerDetail)
        {
            return Channel.UpdateAccountOfficerDetail(accountOfficerDetail);
        }

        public void DeleteAccountOfficerDetail(int accountOfficerDetailId)
        {
            Channel.DeleteAccountOfficerDetail(accountOfficerDetailId);
        }

        public AccountOfficerDetail GetAccountOfficerDetail(int accountOfficerDetailId)
        {
            return Channel.GetAccountOfficerDetail(accountOfficerDetailId);
        }

        public AccountOfficerDetail[] GetAllAccountOfficerDetails()
        {
            return Channel.GetAllAccountOfficerDetails();
        }

        #endregion

        #region BranchDefaultMIS

        public BranchDefaultMIS UpdateBranchDefaultMIS(BranchDefaultMIS branchDefaultMIS)
        {
            return Channel.UpdateBranchDefaultMIS(branchDefaultMIS);
        }

        public void DeleteBranchDefaultMIS(int branchDefaultMISId)
        {
            Channel.DeleteBranchDefaultMIS(branchDefaultMISId);
        }

        public BranchDefaultMIS GetBranchDefaultMIS(int branchDefaultMISId)
        {
            return Channel.GetBranchDefaultMIS(branchDefaultMISId);
        }

        public BranchDefaultMIS[] GetAllBranchDefaultMISs()
        {
            return Channel.GetAllBranchDefaultMISs();
        }

       

        #endregion

        #region ManagementTree

        public ManagementTree UpdateManagementTree(ManagementTree managementTree)
        {
            return Channel.UpdateManagementTree(managementTree);
        }

        public void DeleteManagementTree(int managementTreeId)
        {
            Channel.DeleteManagementTree(managementTreeId);
        }

        public ManagementTree GetManagementTree(int managementTreeId)
        {
            return Channel.GetManagementTree(managementTreeId);
        }

        public ManagementTreeData[] GetAllManagementTrees()
        {
            return Channel.GetAllManagementTrees();
        }


        #endregion

        #region MISReplacement

        public MISReplacement UpdateMISReplacement(MISReplacement misReplacement)
        {
            return Channel.UpdateMISReplacement(misReplacement);
        }

        public void DeleteMISReplacement(int misReplacementId)
        {
            Channel.DeleteMISReplacement(misReplacementId);
        }

        public MISReplacement GetMISReplacement(int misReplacementId)
        {
            return Channel.GetMISReplacement(misReplacementId);
        }

        public MISReplacement[] GetAllMISReplacements()
        {
            return Channel.GetAllMISReplacements();
        }



        #endregion

        #region MPRSetup

        public MPRSetup UpdateMPRSetup(MPRSetup mprMPRSetup)
        {
            return Channel.UpdateMPRSetup(mprMPRSetup);
        }

        public MPRSetup GetFirstMPRSetup()
        {
            return Channel.GetFirstMPRSetup();
        }


        #endregion

        #region TeamClassification

        public TeamClassification UpdateTeamClassification(TeamClassification teamClassification)
        {
            return Channel.UpdateTeamClassification(teamClassification);
        }

        public void DeleteTeamClassification(int teamClassificationId)
        {
            Channel.DeleteTeamClassification(teamClassificationId);
        }

        public TeamClassification GetTeamClassification(int teamClassificationId)
        {
            return Channel.GetTeamClassification(teamClassificationId);
        }

        public TeamClassification[] GetAllTeamClassifications()
        {
            return Channel.GetAllTeamClassifications();
        }


        #endregion

        #region TeamClassificationType

        public TeamClassificationType UpdateTeamClassificationType(TeamClassificationType teamClassificationType)
        {
            return Channel.UpdateTeamClassificationType(teamClassificationType);
        }

        public void DeleteTeamClassificationType(int teamClassificationTypeId)
        {
            Channel.DeleteTeamClassificationType(teamClassificationTypeId);
        }

        public TeamClassificationType GetTeamClassificationType(int teamClassificationTypeId)
        {
            return Channel.GetTeamClassificationType(teamClassificationTypeId);
        }

        public TeamClassificationType[] GetAllTeamClassificationTypes()
        {
            return Channel.GetAllTeamClassificationTypes();
        }


        #endregion

        #region Team

        public Team UpdateTeam(Team team)
        {
            return Channel.UpdateTeam(team);
        }

        public void DeleteTeam(int teamId)
        {
            Channel.DeleteTeam(teamId);
        }

        public Team GetTeam(int teamId)
        {
            return Channel.GetTeam(teamId);
        }

        public TeamData[] GetTeams()
        {
            return Channel.GetTeams();
        }

        public Team[] GetParentTeams(string definitionCode)
        {
            return Channel.GetParentTeams(definitionCode);
        }

        public Team[] GetTeamByLevel(int level)
        {
            return Channel.GetTeamByLevel(level);
        }

        public Team[] GetTeamByDefinition(string definitionCode)
        {
            return Channel.GetTeamByDefinition(definitionCode);
        }

        #endregion

        #region TeamClassificationMap

        public TeamClassificationMap UpdateTeamClassificationMap(TeamClassificationMap teamClassificationMap)
        {
            return Channel.UpdateTeamClassificationMap(teamClassificationMap);
        }

        public void DeleteTeamClassificationMap(int teamClassificationMapId)
        {
            Channel.DeleteTeamClassificationMap(teamClassificationMapId);
        }

        public TeamClassificationMap GetTeamClassificationMap(int teamClassificationMapId)
        {
            return Channel.GetTeamClassificationMap(teamClassificationMapId);
        }

        public TeamClassificationMap[] GetAllTeamClassificationMaps(string misCode,string definitionCode)
        {
            return Channel.GetAllTeamClassificationMaps(misCode, definitionCode);
        }

        #endregion

        #region TransferPrice

        public TransferPrice UpdateTransferPrice(TransferPrice transferPrice)
        {
            return Channel.UpdateTransferPrice(transferPrice);
        }

        public void DeleteTransferPrice(int transferPriceId)
        {
            Channel.DeleteTransferPrice(transferPriceId);
        }

        public TransferPrice GetTransferPrice(int transferPriceId)
        {
            return Channel.GetTransferPrice(transferPriceId);
        }

        public TransferPriceData[] GetAllTransferPrices()
        {
            return Channel.GetAllTransferPrices();
        }


        #endregion

        #region AccountTransferPrice

        public AccountTransferPrice UpdateAccountTransferPrice(AccountTransferPrice accountTransferPrice)
        {
            return Channel.UpdateAccountTransferPrice(accountTransferPrice);
        }

        public void DeleteAccountTransferPrice(int accountTransferPriceId)
        {
            Channel.DeleteAccountTransferPrice(accountTransferPriceId);
        }

        public AccountTransferPrice GetAccountTransferPrice(int accountTransferPriceId)
        {
            return Channel.GetAccountTransferPrice(accountTransferPriceId);
        }

        public AccountTransferPriceData[] GetAllAccountTransferPrices()
        {
            return Channel.GetAllAccountTransferPrices();
        }


        #endregion

        #region GeneralTransferPrice

        public GeneralTransferPrice UpdateGeneralTransferPrice(GeneralTransferPrice generalTransferPrice)
        {
            return Channel.UpdateGeneralTransferPrice(generalTransferPrice);
        }

        public void DeleteGeneralTransferPrice(int generalTransferPriceId)
        {
            Channel.DeleteGeneralTransferPrice(generalTransferPriceId);
        }

        public GeneralTransferPrice GetGeneralTransferPrice(int generalTransferPriceId)
        {
            return Channel.GetGeneralTransferPrice(generalTransferPriceId);
        }

        public GeneralTransferPriceData[] GetAllGeneralTransferPrices()
        {
            return Channel.GetAllGeneralTransferPrices();
        }


        #endregion

        #region CustAccount

        public CustAccount[] GetAllCustAccounts()
        {
            return Channel.GetAllCustAccounts();
        }

        public CustAccount[] GetCustAccounts(string searchType, string searchValue, int number)
        {
            return Channel.GetCustAccounts(searchType,  searchValue,  number);
        }


        #endregion

        #region MemoAccountMap

        public MemoAccountMap UpdateMemoAccountMap(MemoAccountMap memoAccountMap)
        {
            return Channel.UpdateMemoAccountMap(memoAccountMap);
        }

        public void DeleteMemoAccountMap(int memoAccountMapId)
        {
            Channel.DeleteMemoAccountMap(memoAccountMapId);
        }

        public MemoAccountMap GetMemoAccountMap(int memoAccountMapId)
        {
            return Channel.GetMemoAccountMap(memoAccountMapId);
        }

        public MemoAccountMapData[] GetAllMemoAccountMaps()
        {
            return Channel.GetAllMemoAccountMaps();
        }


        #endregion

        #region MemoGLMap

        public MemoGLMap UpdateMemoGLMap(MemoGLMap memoGLMap)
        {
            return Channel.UpdateMemoGLMap(memoGLMap);
        }

        public void DeleteMemoGLMap(int memoGLMapId)
        {
            Channel.DeleteMemoGLMap(memoGLMapId);
        }

        public MemoGLMap GetMemoGLMap(int memoGLMapId)
        {
            return Channel.GetMemoGLMap(memoGLMapId);
        }

        public MemoGLMapData[] GetAllMemoGLMaps()
        {
            return Channel.GetAllMemoGLMaps();
        }


        #endregion

        #region MemoProductMap

        public MemoProductMap UpdateMemoProductMap(MemoProductMap memoProductMap)
        {
            return Channel.UpdateMemoProductMap(memoProductMap);
        }

        public void DeleteMemoProductMap(int memoProductMapId)
        {
            Channel.DeleteMemoProductMap(memoProductMapId);
        }

        public MemoProductMap GetMemoProductMap(int memoProductMapId)
        {
            return Channel.GetMemoProductMap(memoProductMapId);
        }

        public MemoProductMapData[] GetAllMemoProductMaps()
        {
            return Channel.GetAllMemoProductMaps();
        }


        #endregion

        #region MemoUnits

        public MemoUnits UpdateMemoUnits(MemoUnits memoUnits)
        {
            return Channel.UpdateMemoUnits(memoUnits);
        }

        public void DeleteMemoUnits(int memoUnitsId)
        {
            Channel.DeleteMemoUnits(memoUnitsId);
        }

        public MemoUnits GetMemoUnits(int memoUnitsId)
        {
            return Channel.GetMemoUnits(memoUnitsId);
        }

        public MemoUnits[] GetAllMemoUnits()
        {
            return Channel.GetAllMemoUnits();
        }


        #endregion

        #region BSExemption

        public BSExemption UpdateBSExemption(BSExemption bsExemption)
        {
            return Channel.UpdateBSExemption(bsExemption);
        }

        public void DeleteBSExemption(int bsExemptionId)
        {
            Channel.DeleteBSExemption(bsExemptionId);
        }

        public BSExemption GetBSExemption(int bsExemptionId)
        {
            return Channel.GetBSExemption(bsExemptionId);
        }

        public BSExemption[] GetAllBSExemptions()
        {
            return Channel.GetAllBSExemptions();
        }



        #endregion

      

    }
}
