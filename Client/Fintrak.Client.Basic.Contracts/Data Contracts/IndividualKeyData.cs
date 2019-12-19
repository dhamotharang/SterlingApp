using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.Basic.Contracts
{
    [DataContract]
    public class IndividualKeyData : DataContractBase
    {

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public double AmountPrinEnd { get; set; }
        [DataMember]
        public decimal IRR { get; set; }

        [DataMember]
        public double FeeAmount { get; set; }

        public DateTime ValueDate { get; set; }

        public DateTime MaturityDate { get; set; }

        [DataMember]
        public bool Processed { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public string RefNo { get; set; }
    }
}