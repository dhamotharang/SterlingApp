using Fintrak.Client.SystemCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Core
{
    public interface ISecurityAdapter
    {
        void Initialize();

        void Register(string loginID, string password, object propertyValues);
        void Register(UserSetup userSetup);
        bool Login(string loginEmail, string password,string companyCode, bool rememberMe);
        bool ChangePassword(string loginEmail, string oldPassword, string newPassword);
        bool UserExists(string loginEmail);
        void LogOut();
    }
}
