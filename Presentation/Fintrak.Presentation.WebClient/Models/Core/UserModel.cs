using Fintrak.Client.Core.Contracts;
using Fintrak.Client.SystemCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class UserModel
    {
        public UserSetup UserSetup { get; set; }
        public UserGroupModel[] Roles { get; set; }
        public UserGroupModel[] ReportRoles { get; set; }
        public UserCompanyModel[] UserCompanies { get; set; }
    }
}