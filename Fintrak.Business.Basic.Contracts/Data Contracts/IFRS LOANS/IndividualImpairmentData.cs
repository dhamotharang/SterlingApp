using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.Basic.Contracts
{
    [DataContract]
    public class IndividualImpairmentData : DataContractBase
    {
         [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public DateTime ValueDate { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public bool Processed { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
