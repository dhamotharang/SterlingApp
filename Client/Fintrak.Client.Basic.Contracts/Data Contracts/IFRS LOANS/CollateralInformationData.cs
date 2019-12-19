using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Client.Basic.Contracts
{
    [DataContract]
    public class CollateralInformationData : DataContractBase
    {
        [DataMember]
        public int CollateralInformationId { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
