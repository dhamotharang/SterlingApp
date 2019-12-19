using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Budget.Contracts;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Budget.Proxies
{
    [Export(typeof(ITeamService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamClient : UserClientBase<ITeamService>, ITeamService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

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

        //public TeamData[] GetTeams()
        //{
        //    return Channel.GetTeams();
        //}

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

        #region TeamUser

        public TeamUser UpdateTeamUser(TeamUser teamUser)
        {
            return Channel.UpdateTeamUser(teamUser);
        }

        public void DeleteTeamUser(int teamUserId)
        {
            Channel.DeleteTeamUser(teamUserId);
        }

        public TeamUser GetTeamUser(int teamUserId)
        {
            return Channel.GetTeamUser(teamUserId);
        }

        public TeamUser GetTeamUserByLoginID(string loginID)
        {
            return Channel.GetTeamUserByLoginID(loginID);
        }

        public TeamUser[] GetAllTeamUsers()
        {
            return Channel.GetAllTeamUsers();
        }



        #endregion

        #region TeamSetting

        public TeamSetting UpdateTeamSetting(TeamSetting teamSetting)
        {
            return Channel.UpdateTeamSetting(teamSetting);
        }

        public void DeleteTeamSetting(int teamSettingId)
        {
            Channel.DeleteTeamSetting(teamSettingId);
        }

        public TeamSetting GetTeamSetting(int teamSettingId)
        {
            return Channel.GetTeamSetting(teamSettingId);
        }

        public TeamSetting[] GetAllTeamSettings()
        {
            return Channel.GetAllTeamSettings();
        }

        #endregion

        #region OfficerDetail

        public OfficerDetail UpdateOfficerDetail(OfficerDetail officerDetail)
        {
            return Channel.UpdateOfficerDetail(officerDetail);
        }

        public void DeleteOfficerDetail(int officerDetailId)
        {
            Channel.DeleteOfficerDetail(officerDetailId);
        }

        public OfficerDetail GetOfficerDetail(int officerDetailId)
        {
            return Channel.GetOfficerDetail(officerDetailId);
        }

        public OfficerDetail[] GetAllOfficerDetails()
        {
            return Channel.GetAllOfficerDetails();
        }

        #endregion

        

    }
}
