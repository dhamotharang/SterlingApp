using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Basic.Framework;


namespace Fintrak.Client.Basic.Contracts
{
    [DataContract]
    public class MPRProductData : DataContractBase
    {
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string CaptionCode { get; set; }

        [DataMember]
        public string CaptionName { get; set; }

        [DataMember]
        public BalanceSheetCategory Category { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public CurrencyType CurrencyType { get; set; }

        [DataMember]
        public string CurrencyTypeName { get; set; }

        [DataMember]
        public string VolumeGL { get; set; }

        [DataMember]
        public string InterestGL { get; set; }

        [DataMember]
        public bool Budgetable { get; set; }

        [DataMember]
        public bool IsNotional { get; set; } 

       [DataMember]
        public bool Active { get; set; }

       [DataMember]
       public string LongDescription { get; set; }
    }
}
