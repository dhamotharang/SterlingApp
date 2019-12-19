using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Client.IFRS.Contracts
{
    [DataContract]
    public class PostingDetailData2 : DataContractBase
    {
        [DataMember]
        public int PostingDetailId { get; set; }

        [DataMember]
        public string TransactionDesc  { get; set; }

        [DataMember]
        public string Indicator { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public decimal GaapAmount { get; set; }

        [DataMember]
        public decimal IFRSAmount { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
