using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Entities;
using coreEntities = Fintrak.Client.SystemCore.Entities;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.SystemCore.Framework;

namespace Fintrak.Client.SystemCore.Proxies
{
    [Export(typeof(ICoreService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CoreClient : UserClientBase<ICoreService>, ICoreService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region Solution

        public Solution UpdateSolution(Solution solution)
        {
            return Channel.UpdateSolution(solution);
        }

        public void DeleteSolution(int solutionId)
        {
            Channel.DeleteSolution(solutionId);
        }

        public Solution GetSolution(int solutionId)
        {
            return Channel.GetSolution(solutionId);
        }

        public Solution[] GetAllSolutions()
        {
            return Channel.GetAllSolutions();
        }


        #endregion

        #region Module

        public Module UpdateModule(Module module)
        {
            return Channel.UpdateModule(module);
        }

        public void DeleteModule(int moduleId)
        {
            Channel.DeleteModule(moduleId);
        }

        public Module GetModule(int moduleId)
        {
            return Channel.GetModule(moduleId);
        }

        public Module[] GetAllModules()
        {
            return Channel.GetAllModules();
        }

        public ModuleData[] GetModules()
        {
            return Channel.GetModules();
        }


        #endregion

        #region Role

        public Role UpdateRole(Role role)
        {
            return Channel.UpdateRole(role);
        }

        public void DeleteRole(int roleId)
        {
            Channel.DeleteRole(roleId);
        }

        public Role GetRole(int roleId)
        {
            return Channel.GetRole(roleId);
        }

        public RoleData[] GetAllRoles()
        {
            return Channel.GetAllRoles();
        }


        #endregion

        #region UserSetup

        public UserSetup UpdateUserSetup(UserSetup userSetup)
        {
            return Channel.UpdateUserSetup(userSetup);
        }

        public UserSetup UpdateUserSetupProfile(UserSetup userSetup)
        {
            return Channel.UpdateUserSetupProfile(userSetup);
        }

        public void DeleteUserSetup(int userSetupId)
        {
            Channel.DeleteUserSetup(userSetupId);
        }

        public UserSetup GetUserSetup(int userSetupId)
        {
            return Channel.GetUserSetup(userSetupId);
        }

        public UserSetup GetUserSetupByLoginID(string loginID)
        {
            return Channel.GetUserSetupByLoginID(loginID);
        }

        public UserSetup[] GetAllUserSetups()
        {
            return Channel.GetAllUserSetups();
        }

        public UserSetup[] ConfirmDefaultUser()
        {
            return Channel.ConfirmDefaultUser();
        }

        public void AssignDefaultRole(UserSetup account)
        {
            Channel.AssignDefaultRole(account);
        }

        #endregion

        #region UserRole

        public UserRole UpdateUserRole(UserRole userRole)
        {
            return Channel.UpdateUserRole(userRole);
        }

        public void DeleteUserRole(int userRoleId)
        {
            Channel.DeleteUserRole(userRoleId);
        }

        public UserRole GetUserRole(int userRoleId)
        {
            return Channel.GetUserRole(userRoleId);
        }

        public UserRole[] GetAllUserRoles()
        {
            return Channel.GetAllUserRoles();
        }

        public UserRoleData[] GetUserRoleByLoginID(string loginID)
        {
            return Channel.GetUserRoleByLoginID(loginID);
        }

        public UserRoleData[] GetUserRoles()
        {
            return Channel.GetUserRoles();
        }


        #endregion

        #region Menu

        public Menu UpdateMenu(Menu menu)
        {
            return Channel.UpdateMenu(menu);
        }

        public void DeleteMenu(int menuId)
        {
            Channel.DeleteMenu(menuId);
        }

        public Menu GetMenu(int menuId)
        {
            return Channel.GetMenu(menuId);
        }

        public Menu[] GetAllMenus()
        {
            return Channel.GetAllMenus();
        }

        public MenuData[] GetMenuByLoginID(string loginID)
        {
            return Channel.GetMenuByLoginID(loginID);
        }

        public MenuData[] GetMenus()
        {
            return Channel.GetMenus();
        }


        #endregion

        #region MenuRole

        public MenuRole UpdateMenuRole(MenuRole menuRole)
        {
            return Channel.UpdateMenuRole(menuRole);
        }

        public void DeleteMenuRole(int menuRoleId)
        {
            Channel.DeleteMenuRole(menuRoleId);
        }

        public MenuRole GetMenuRole(int menuRoleId)
        {
            return Channel.GetMenuRole(menuRoleId);
        }

        public MenuRole[] GetAllMenuRoles()
        {
            return Channel.GetAllMenuRoles();
        }

        public MenuRoleData[] GetMenuRoles()
        {
            return Channel.GetMenuRoles();
        }

        public MenuRoleData[] GetMenuRoleByLoginID(string loginID)
        {
            return Channel.GetMenuRoleByLoginID(loginID);
        }


        #endregion

        #region AuditTrail

        public AuditTrail UpdateAuditTrail(AuditTrail auditTrail)
        {
            return Channel.UpdateAuditTrail(auditTrail);
        }

        public void DeleteAuditTrail(long auditTrailId)
        {
            Channel.DeleteAuditTrail(auditTrailId);
        }

        public AuditTrail GetAuditTrail(long auditTrailId)
        {
            return Channel.GetAuditTrail(auditTrailId);
        }

        public AuditTrail[] GetAllAuditTrails()
        {
            return Channel.GetAllAuditTrails();
        }

        public AuditTrail[] GetAuditTrails(DateTime fromDate, DateTime toDate)
        {
            return Channel.GetAuditTrails(fromDate, toDate);
        }

        public AuditTrail[] GetAuditTrailByTable(string tableName, DateTime fromDate, DateTime toDate)
        {
            return Channel.GetAuditTrailByTable(tableName, fromDate, toDate);
        }

        public AuditTrail[] GetAuditTrailByLoginID(string userName, DateTime fromDate, DateTime toDate)
        {
            return Channel.GetAuditTrailByLoginID(userName, fromDate, toDate);
        }

        public AuditTrail[] GetAuditTrailByAction(string action, DateTime fromDate, DateTime toDate)
        {
            return Channel.GetAuditTrailByAction(action, fromDate, toDate);
        }

        public AuditTrail[] GetAuditTrailByTab(AuditAction action)
        {
            return Channel.GetAuditTrailByTab(action);
        }

        #endregion

        #region Company

        public Company UpdateCompany(Company company)
        {
            return Channel.UpdateCompany(company);
        }

        public void DeleteCompany(int companyId)
        {
            Channel.DeleteCompany(companyId);
        }

        public Company GetCompany(int companyId)
        {
            return Channel.GetCompany(companyId);
        }

        public Company[] GetAllCompanies()
        {
            return Channel.GetAllCompanies();
        }


        #endregion

        #region General

        public General UpdateGeneral(General general)
        {
            return Channel.UpdateGeneral(general);
        }

        public void DeleteGeneral(int generalId)
        {
            Channel.DeleteGeneral(generalId);
        }

        public General GetGeneral(int generalId)
        {
            return Channel.GetGeneral(generalId);
        }

        public General[] GetAllGenerals()
        {
            return Channel.GetAllGenerals();
        }

        public General GetFirstGeneral()
        {
            return Channel.GetFirstGeneral();
        }


        #endregion

        #region Log Event

        public LogEvent GetLogById(string id)
        {
            return Channel.GetLogById(id);
        }

        public LogEvent[] GetLogByDateRangeAndType(DateTime start, DateTime end, string logLevel)
        {
            return Channel.GetLogByDateRangeAndType(start, end, logLevel);
        }

        public void ClearLog(DateTime start, DateTime end, string[] logLevels)
        {
            Channel.ClearLog(start, end, logLevels);
        }

        #endregion

        #region Client

        public coreEntities.Client UpdateClient(coreEntities.Client client)
        {
            return Channel.UpdateClient(client);
        }

        public void DeleteClient(int clientId)
        {
            Channel.DeleteClient(clientId);
        }

        public coreEntities.Client GetClient(int clientId)
        {
            return Channel.GetClient(clientId);
        }

        public coreEntities.Client[] GetAllClients()
        {
            return Channel.GetAllClients();
        }


        #endregion

        #region Database

        public Database UpdateDatabase(Database database)
        {
            return Channel.UpdateDatabase(database);
        }

        public void DeleteDatabase(int databaseId)
        {
            Channel.DeleteDatabase(databaseId);
        }

        public Database GetDatabase(int databaseId)
        {
            return Channel.GetDatabase(databaseId);
        }

        public DatabaseData[] GetAllDatabases()
        {
            return Channel.GetAllDatabases();
        }


        #endregion

        #region CompanySecurity

        public CompanySecurity UpdateCompanySecurity(CompanySecurity companySecurity)
        {
            return Channel.UpdateCompanySecurity(companySecurity);
        }

        public void DeleteCompanySecurity(int companySecurityId)
        {
            Channel.DeleteCompanySecurity(companySecurityId);
        }

        public CompanySecurity GetCompanySecurity(int companySecurityId)
        {
            return Channel.GetCompanySecurity(companySecurityId);
        }

        public CompanySecurity[] GetAllCompanySecuritys()
        {
            return Channel.GetAllCompanySecuritys();
        }


        #endregion

        #region UserSession

        public UserSession UpdateUserSession(UserSession userSession)
        {
            return Channel.UpdateUserSession(userSession);
        }

        public void DeleteUserSession(int userSessionId)
        {
            Channel.DeleteUserRole(userSessionId);
        }

        public UserSession GetUserSession(int userSessionId)
        {
            return Channel.GetUserSession(userSessionId);
        }

        public UserSessionData[] GetUserSessionByLoginID(string loginID)
        {
            return Channel.GetUserSessionByLoginID(loginID);
        }

        #endregion

        #region CompanyUser

        public CompanyUser UpdateCompanyUser(CompanyUser companyUser)
        {
            return Channel.UpdateCompanyUser(companyUser);
        }

        public void DeleteCompanyUser(int companyUserId)
        {
            Channel.DeleteCompanyUser(companyUserId);
        }

        public CompanyUser GetCompanyUser(int companyUserId)
        {
            return Channel.GetCompanyUser(companyUserId);
        }

        public CompanyUser[] GetAllCompanyUsers()
        {
            return Channel.GetAllCompanyUsers();
        }

        public CompanyUser GetCompanyUsers(string loginID, string companyCode)
        {
            return Channel.GetCompanyUsers(loginID, companyCode);
        }


        public CompanyUser[] GetCompanyUserByLogin(string loginID)
        {
            return Channel.GetCompanyUserByLogin(loginID);
        }


        #endregion

        #region CompanyModule

        public CompanyModule UpdateCompanyModule(CompanyModule companyModule)
        {
            return Channel.UpdateCompanyModule(companyModule);
        }

        public void DeleteCompanyModule(int companyModuleId)
        {
            Channel.DeleteCompanyModule(companyModuleId);
        }

        public CompanyModule GetCompanyModule(int companyModuleId)
        {
            return Channel.GetCompanyModule(companyModuleId);
        }

        public CompanyModule[] GetAllCompanyModules()
        {
            return Channel.GetAllCompanyModules();
        }

        public CompanyModuleData[] GetCompanyModuleByCompanyCode(string companyCode)
        {
            return Channel.GetCompanyModuleByCompanyCode(companyCode);
        }

        public CompanyModuleData[] GetCompanyModules()
        {
            return Channel.GetCompanyModules();
        }

        #endregion

        #region ErrorTracker

        public ErrorTracker UpdateErrorTracker(ErrorTracker errorTracker)
        {
            return Channel.UpdateErrorTracker(errorTracker);
        }

        public void DeleteErrorTracker(int errorTrackerId)
        {
            Channel.DeleteErrorTracker(errorTrackerId);
        }

        public void DeleteAllErrorTracker()
        {
            Channel.DeleteAllErrorTracker();
        }

        public ErrorTracker GetErrorTracker(int errorTrackerId)
        {
            return Channel.GetErrorTracker(errorTrackerId);
        }

        public ErrorTracker[] GetAllErrorTrackers()
        {
            return Channel.GetAllErrorTrackers();
        }

        #endregion


        public bool IsNewSystem(string companyCode)
        {
            return Channel.IsNewSystem(companyCode);
        }

        public bool IsFirstLogon(string loginId)
        {
            return Channel.IsFirstLogon(loginId);
        }
    }
}
