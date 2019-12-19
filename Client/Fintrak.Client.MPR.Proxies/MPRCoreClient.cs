using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Shared.Common.ServiceModel;
using System.Collections.Generic;

namespace Fintrak.Client.MPR.Proxies
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

        #region UserClassificationMap

        public UserClassificationMap UpdateUserClassificationMap(UserClassificationMap userClassificationMap)
        {
            return Channel.UpdateUserClassificationMap(userClassificationMap);
        }

        public void DeleteUserClassificationMap(int userClassificationMapId)
        {
            Channel.DeleteUserClassificationMap(userClassificationMapId);
        }

        public UserClassificationMap GetUserClassificationMap(int userClassificationMapId)
        {
            return Channel.GetUserClassificationMap(userClassificationMapId);
        }

        public UserClassificationMap[] GetAllUserClassificationMaps(string loginID)
        {
            return Channel.GetAllUserClassificationMaps(loginID);
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

        public IEnumerable<TeamDefinition> GetAllTeamDefinitions()
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

        public void DeleteSelectedIds(string selectedIds)
        {
            Channel.DeleteSelectedIds(selectedIds);
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

        public Setup UpdateMPRSetup(Setup mprMPRSetup)
        {
            return Channel.UpdateMPRSetup(mprMPRSetup);
        }

        public Setup GetFirstMPRSetup()
        {
            return Channel.GetFirstMPRSetup();
        }

        public MPRSetupData[] GetFirstMPRSetups()
        {
            return Channel.GetFirstMPRSetups();
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

        public TeamClassification[] GetTeamClassifications(string typeCode)
        {
            return Channel.GetTeamClassifications(typeCode);
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

        public IEnumerable<Team> GetTeamByDefinition(string definitionCode)
        {
            return Channel.GetTeamByDefinition(definitionCode);
        }

        public TeamData[] GetTeamsBySearch(string SearchValue)
        {
            return Channel.GetTeamsBySearch(SearchValue);
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

        public TeamClassificationMap[] GetAllTeamClassificationMaps(string misCode, string definitionCode)
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

        public void DeleteGTPSelectedIds(string selectedIds)
        {
            Channel.DeleteGTPSelectedIds(selectedIds);
        }
        #endregion

        #region CustAccount

        public CustAccount[] GetAllCustAccounts()
        {
            return Channel.GetAllCustAccounts();
        }

        public CustAccount[] GetCustAccounts(string searchType, string searchValue, int number)
        {
            return Channel.GetCustAccounts(searchType, searchValue, number);
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

        #region CaptionMapping

        public CaptionMapping UpdateCaptionMapping(CaptionMapping captionMapping)
        {
            return Channel.UpdateCaptionMapping(captionMapping);
        }

        public void DeleteCaptionMapping(int captionMappingId)
        {
            Channel.DeleteCaptionMapping(captionMappingId);
        }

        public CaptionMapping GetCaptionMapping(int captionMappingId)
        {
            return Channel.GetCaptionMapping(captionMappingId);
        }

        public CaptionMapping[] GetAllCaptionMappings()
        {
            return Channel.GetAllCaptionMappings();
        }

        #endregion

        #region RatioCaptionMapping

        public RatioCaptionMapping UpdateRatioCaptionMapping(RatioCaptionMapping ratioCaptionMapping)
        {
            return Channel.UpdateRatioCaptionMapping(ratioCaptionMapping);
        }

        public void DeleteRatioCaptionMapping(int ratioCaptionMappingId)
        {
            Channel.DeleteRatioCaptionMapping(ratioCaptionMappingId);
        }

        public RatioCaptionMapping GetRatioCaptionMapping(int ratioCaptionMappingId)
        {
            return Channel.GetRatioCaptionMapping(ratioCaptionMappingId);
        }

        public RatioCaptionMapping[] GetAllRatioCaptionMappings()
        {
            return Channel.GetAllRatioCaptionMappings();
        }

        #endregion

        #region Ratios

        public Ratios UpdateRatios(Ratios ratios)
        {
            return Channel.UpdateRatios(ratios);
        }

        public void DeleteRatios(int ratiosId)
        {
            Channel.DeleteRatios(ratiosId);
        }

        public Ratios GetRatios(int ratiosId)
        {
            return Channel.GetRatios(ratiosId);
        }

        public Ratios[] GetAllRatios()
        {
            return Channel.GetAllRatios();
        }

        #endregion

        #region AbcRatio

        public AbcRatio UpdateAbcRatio(AbcRatio abcRatio)
        {
            return Channel.UpdateAbcRatio(abcRatio);
        }

        public void DeleteAbcRatio(int abcRatioId)
        {
            Channel.DeleteAbcRatio(abcRatioId);
        }

        public AbcRatio GetAbcRatio(int abcRatioId)
        {
            return Channel.GetAbcRatio(abcRatioId);
        }

        public AbcRatio[] GetAllAbcRatio()
        {
            return Channel.GetAllAbcRatio();
        }

        #endregion

        #region Sbu

        public Sbu UpdateSbu(Sbu sbu)
        {
            return Channel.UpdateSbu(sbu);
        }

        public void DeleteSbu(int sbuId)
        {
            Channel.DeleteSbu(sbuId);
        }

        public Sbu GetSbu(int sbuId)
        {
            return Channel.GetSbu(sbuId);
        }

        public Sbu[] GetAllSbu()
        {
            return Channel.GetAllSbu();
        }

        #endregion

        #region SbuType

        public SbuType UpdateSbuType(SbuType sbuType)
        {
            return Channel.UpdateSbuType(sbuType);
        }

        public void DeleteSbuType(int sbuTypeId)
        {
            Channel.DeleteSbuType(sbuTypeId);
        }

        public SbuType GetSbuType(int sbuTypeId)
        {
            return Channel.GetSbuType(sbuTypeId);
        }

        public SbuType[] GetAllSbuType()
        {
            return Channel.GetAllSbuType();
        }

        #endregion

        #region Servicese

        public Servicese UpdateServices(Servicese services)
        {
            return Channel.UpdateServices(services);
        }

        public void DeleteServices(int servicesId)
        {
            Channel.DeleteServices(servicesId);
        }

        public Servicese GetServices(int servicesId)
        {
            return Channel.GetServices(servicesId);
        }

        public Servicese[] GetAllServices()
        {
            return Channel.GetAllServices();
        }

        #endregion

        #region Staffs

        public Staffs UpdateStaffs(Staffs staffs)
        {
            return Channel.UpdateStaffs(staffs);
        }

        public void DeleteStaffs(int staffId)
        {
            Channel.DeleteStaffs(staffId);
        }

        public Staffs GetStaffs(int staffId)
        {
            return Channel.GetStaffs(staffId);
        }

        public Staffs[] GetAllStaffs()
        {
            return Channel.GetAllStaffs();
        }


        #endregion

        #region MessagingSubscription

        public MessagingSubscription UpdateMessagingSubscription(MessagingSubscription messagingSubscription)
        {
            return Channel.UpdateMessagingSubscription(messagingSubscription);
        }

        public void DeleteMessagingSubscription(int messagingSubscriptionId)
        {
            Channel.DeleteMessagingSubscription(messagingSubscriptionId);
        }

        public MessagingSubscription GetMessagingSubscription(int messagingSubscriptionId)
        {
            return Channel.GetMessagingSubscription(messagingSubscriptionId);
        }


        public Revenue[] GetMessagingSubscriptionByRecipients(string recipients)
        {
            return Channel.GetMessagingSubscriptionByRecipients(recipients);
        }


        public DateTime[] GetRecipents()
        {
            return Channel.GetRecipents();
        }


        public string[] GetReports()
        {
            return Channel.GetReports();
        }

        #endregion


    }
}
