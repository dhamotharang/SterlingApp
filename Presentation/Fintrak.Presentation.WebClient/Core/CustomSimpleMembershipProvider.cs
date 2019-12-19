using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace Fintrak.Presentation.WebClient.Core
{
    public class CustomSimpleMembershipProvider : SimpleMembershipProvider
    {
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return base.GetUser(username, userIsOnline);
        }

        public override bool ValidateUser(string username, string password)
        {
            if (base.ValidateUser(username, password))
            {
                return true;
            }

            return false;
        }
    }
}