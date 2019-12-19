using System.Web;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Utils;
using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Web.Security;
using WebMatrix.WebData;
using Fintrak.Presentation.WebClient.Models;
using System.Web.Script.Serialization;
using System.ServiceModel;

namespace Fintrak.Presentation.WebClient.Services
{
    [Export(typeof(ISecurityAdapter))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SecurityAdapter : ISecurityAdapter
    {
        public void Initialize()
        {
            var securityMode = ConfigurationManager.AppSettings["SecurityMode"].ToString();

            if (securityMode == "UP")
            {
                if (!WebSecurity.Initialized)
                    WebSecurity.InitializeDatabaseConnection("FintrakCoreDBConnection", "cor_usersetup", "UserSetupId", "LoginID", autoCreateTables: true);

                InitializeRolesAndUsers();
            }
           
        }

        [Import]
        ICoreService coreService;

        private void InitializeRolesAndUsers()
        {
            try
            {
                //create manager
                if (!WebSecurity.UserExists("fintrak"))
                {

                    WebSecurity.CreateUserAndAccount("fintrak", "@password",
                       new
                       {
                           Name = "fintrak",
                           Email = "fintrak@fintraksoftware.com",
                           MultiCompanyAccess = true,
                           LatestConnection = DateTime.Now,
                           Active = true,
                           Deleted = false,
                           CreatedBy = "Auto",
                           CreatedOn = DateTime.Now,
                           UpdatedBy = "Auto",
                           UpdatedOn = DateTime.Now
                       });  
                }

                //create business
                if (!WebSecurity.UserExists("fintrakbusiness"))
                {

                    WebSecurity.CreateUserAndAccount("fintrakbusiness", "@password",
                       new
                       {
                           Name = "fintrakbusiness",
                           Email = "fintrakbusiness@fintraksoftware.com",
                           MultiCompanyAccess = true,
                           LatestConnection = DateTime.Now,
                           Active = true,
                           Deleted = false,
                           CreatedBy = "Auto",
                           CreatedOn = DateTime.Now,
                           UpdatedBy = "Auto",
                           UpdatedOn = DateTime.Now
                       });
                }

                //create user
                if (!WebSecurity.UserExists("fintrakuser"))
                {

                    WebSecurity.CreateUserAndAccount("fintrakuser", "@password",
                       new
                       {
                           Name = "fintrakuser",
                           Email = "fintrakuser@fintraksoftware.com",
                           MultiCompanyAccess = true,
                           LatestConnection = DateTime.Now,
                           Active = true,
                           Deleted = false,
                           CreatedBy = "Auto",
                           CreatedOn = DateTime.Now,
                           UpdatedBy = "Auto",
                           UpdatedOn = DateTime.Now
                       });
                }

                //Check fintrak's role
                coreService.ConfirmDefaultUser();
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public void Register(string loginID, string password, object propertyValues)
        {
            WebSecurity.CreateUserAndAccount(loginID, password, propertyValues);
        }

        public void Register(UserSetup model)
        {
            //create user
            if (!WebSecurity.UserExists(model.LoginID))
            {

                WebSecurity.CreateUserAndAccount(model.LoginID, "@password",
                   new
                   {
                       Name = model.LoginID,
                       Email = model.Email,
                       MultiCompanyAccess = model.MultiCompanyAccess,
                       LatestConnection = DateTime.Now,
                       Active = true,
                       Deleted = false,
                       CreatedBy = "Auto",
                       CreatedOn = DateTime.Now,
                       UpdatedBy = "Auto",
                       UpdatedOn = DateTime.Now
                   });
            }
        }


        public bool Login(string loginID, string password, string companyCode, bool rememberMe)
        {
            bool success = false;
            bool canContinue = false;

            canContinue = coreService.IsNewSystem(companyCode);
            if (!canContinue)
                throw new Exception("Enter your company code to login.");

            var securityMode = ConfigurationManager.AppSettings["SecurityMode"].ToString();
            if (securityMode == "UP")
            {
                success = WebSecurity.Login(loginID, password, persistCookie: false);
            }
            else
            {
                if (Membership.ValidateUser(loginID, password))
                {
                    FormsAuthentication.SetAuthCookie(loginID, false);
                    success =  true;
                }
            }

            if (success)
            {
                AccountLoginModel serializeModel = new AccountLoginModel();
                serializeModel.CompanyCode = companyCode;

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string userData = serializer.Serialize(serializeModel);

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, loginID, DateTime.Now, DateTime.Now.AddHours(10), false, userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                faCookie.Expires = false ? authTicket.Expiration : DateTime.Now.AddHours(10);
                HttpContext.Current.Response.Cookies.Add(faCookie);

                var user = coreService.GetUserSetupByLoginID(loginID);
                if (user != null)
                {
                    user.LatestConnection = DateTime.Now;
                    coreService.UpdateUserSetupProfile(user);

                    //EventLogger.LogInformation(string.Format("User: {0} login successfull. - {1}", loginID, DateTime.Now.ToLongDateString()), Constants.FINTRAK_ENTERPRISE);
                }
            }
            else
               // EventLogger.LogWarning(string.Format("User: {0} login operation failed. - {1}", loginID, DateTime.Now.ToLongDateString()), Constants.FINTRAK_ENTERPRISE);
            if (success != true)
                throw new Exception("Wrong UserName or Password.");
            return success;
        }

        public bool ChangePassword(string loginID, string oldPassword, string newPassword)
        {
            return WebSecurity.ChangePassword(loginID, oldPassword, newPassword);
        }

        public bool UserExists(string loginID)
        {
            var securityMode = ConfigurationManager.AppSettings["SecurityMode"].ToString();
            if (securityMode == "UP")
                return WebSecurity.UserExists(loginID);
            else
            {
                return Membership.GetUser(loginID) != null;
            }
            
        }

        public bool UserExistInRole(string loginEmail, string roleName)
        {
            return Roles.GetRolesForUser(loginEmail).Contains(roleName);
        }

        public void AddUserToRole(string userName, string roleName)
        {
            if (!Roles.GetRolesForUser(userName).Contains(roleName))
                Roles.AddUsersToRole(new[] { userName }, roleName);
        }

        public void LogOut()
        {
            var securityMode = ConfigurationManager.AppSettings["SecurityMode"].ToString();
            if (securityMode == "UP")
                WebSecurity.Logout();
            else
                FormsAuthentication.SignOut();

        }

       
    }
}
