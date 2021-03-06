using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Client.IFRS.Contracts
{
    [DataContract]
    public class IndividualScheduleData : DataContractBase
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public decimal IRR { get; set; }

        [DataMember]
        public double AmountPrinEnd { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public DateTime ValueDate { get; set; }

        [DataMember]
        public Nullable<DateTime> FirstRepaymentdate { get; set; }

        [DataMember]
        public double PastDueAmount { get; set; }

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
