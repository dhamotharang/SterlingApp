using System;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class CheckListData : DataContractBase
    {

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public double ACTUAL { get; set; }

        [DataMember]
        public string SOURCE { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string CAPTION { get; set; }
    }
}