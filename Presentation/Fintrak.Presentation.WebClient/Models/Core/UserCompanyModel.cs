using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Entities;
using System;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class UserCompanyModel
    {
        public int UserSetupId { get; set; }
        public string LoginID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public bool IsChecked { get; set; }
    }
}