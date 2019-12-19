using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Basic.Contracts
{
    [DataContract]
    public class TeamData : DataContractBase
    {
        [DataMember]
        public int TeamId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public string DefinitionCode { get; set; }

        [DataMember]
        public Nullable<int> Period { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
