using System;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.IFRS.Contracts
{
    [DataContract]
    public class KeyValueData : DataContractBase
    {

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}