using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.CloudClient.Domain
{
    public partial class Company
    {
        public string DemoMode
        {
            get
            {
                if (IsDemo)
                    return "End Demo";
                else
                    return "Start Demo";
            }
        }

        public string ActivationMode
        {
            get
            {
                if (IsActivated)
                    return "Deactivate";
                else
                    return "Activate";
            }
        }

        public string CountDownMode
        {
            get
            {
                if (IsActivated )
                    return "alert-info";

                if (DemoEndDate.HasValue && DemoStartDate.HasValue)
                {
                    var countDown = (DemoEndDate.Value - DemoStartDate.Value).TotalDays;

                    if (countDown <= 30)
                        return "alert-danger";
                    else if (countDown <= 60)
                        return "alert-warning";
                    else
                        return "alert-success";
                } 
                else
                    return "alert-info";
            }
        }

        public double CountDown
        {
            get
            {
                if (IsActivated)
                    return 0;

                if (DemoEndDate.HasValue && DemoStartDate.HasValue)
                    return (DemoEndDate.Value - DemoStartDate.Value).TotalDays;
                else
                    return 0;
                
            }
        }
    }
}