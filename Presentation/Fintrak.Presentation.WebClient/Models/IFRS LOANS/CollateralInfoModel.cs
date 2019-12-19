
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class CollateralInfoModel
    {
        public CollateralInformation CollateralInformation { get; set; }
        public CollateralCategory CollateralCategory { get; set; }
        public CollateralTypeData[] CollateralType { get; set; }
    }
}