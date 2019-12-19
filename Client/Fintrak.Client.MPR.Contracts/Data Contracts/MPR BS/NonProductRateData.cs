using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Framework;


namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class NonProductRateData : DataContractBase
    {
        [DataMember]
        public int NonProductRateId { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public BalanceSheetCategory Category { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public CurrencyType CurrencyType { get; set; }

        [DataMember]
        public string CurrencyTypeName { get; set; }

        [DataMember]
        public string Period { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public double Rate { get; set; }

       [DataMember]
        public bool Active { get; set; }
    }
}
