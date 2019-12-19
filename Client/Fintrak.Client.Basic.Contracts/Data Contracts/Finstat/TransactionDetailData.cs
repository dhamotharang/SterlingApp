using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Client.Basic.Contracts
{
    [DataContract]
    public class TransactionDetailData : DataContractBase
    {
        [DataMember]
        public int TransactionDetailId { get; set; }

        [DataMember]
        public string GLCode  { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
