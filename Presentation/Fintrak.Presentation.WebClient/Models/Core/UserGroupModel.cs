using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Entities;
using System;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class UserGroupModel
    {
        public int UserSetupId { get; set; }
        public string LoginID { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int SolutionId { get; set; }
        public string SolutionName { get; set; }
        public RoleData[] Roles { get; set; }
    }
}