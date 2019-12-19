using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class AccountChangePasswordModel
    {
        public string LoginID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }
}
