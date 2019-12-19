using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class IncomeCentralVaultScheduleData : DataContractBase
    {

        [DataMember]

        public int IncomeCentralVaultScheduleId { get; set; }

        [DataMember]   
        public string BranchCode { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember] 
        public string Currency { get; set; }

        [DataMember]
        public DateTime DatePosted { get; set; }

        [DataMember]
        public double Volume { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}