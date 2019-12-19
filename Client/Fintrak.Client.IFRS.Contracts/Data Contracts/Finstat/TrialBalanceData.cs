using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Client.IFRS.Contracts
{
    [DataContract]
    public class TrialBalanceData : DataContractBase
    {
        [DataMember]
        public int TrialBalanceId { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        public string GLCode  { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string GLSubHeadCode { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public double ExchangeRate { get; set; }

        [DataMember]
        public decimal Debit{ get; set; }

        [DataMember]
        public decimal Credit { get; set; }

        [DataMember]
        public decimal LCYDebit { get; set; }

        [DataMember]
        public decimal LCYCredit { get; set; }

        [DataMember]
        public decimal LCYBalance { get; set; }

        [DataMember]
        public decimal Balance { get; set; }

        [DataMember]
        public string GLType { get; set; }

        [DataMember]
        public decimal RevaluationDiff { get; set; }

        [DataMember]
        public DateTime TransDate{ get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string SubGL { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
