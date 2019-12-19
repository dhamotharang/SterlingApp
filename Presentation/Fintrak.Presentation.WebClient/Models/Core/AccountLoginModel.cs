using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class AccountLoginModel

    {
        public string CompanyCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }

}