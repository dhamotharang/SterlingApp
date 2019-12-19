using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Client.Basic.Contracts
{
    [DataContract]
    public class MPRTotalLineMakeUpData : DataContractBase
    {
        [DataMember]
        public int MPRTotalLineMakeUpId { get; set; }

        [DataMember]
        public string TotalLine { get; set; }

        [DataMember]
        public string CaptionCode { get; set; }

        [DataMember]
        public string CaptionName { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
