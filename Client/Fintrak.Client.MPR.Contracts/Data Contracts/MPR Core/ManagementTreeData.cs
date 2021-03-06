using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class ManagementTreeData : DataContractBase
    {
        [DataMember]
        public int ManagementTreeId { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string TeamDefinitionCode { get; set; }

        [DataMember]
        public string TeamDefinitionName { get; set; }

        [DataMember]
        public string TeamCode { get; set; }

        [DataMember]
        public string TeamName { get; set; }

        [DataMember]
        public string AccountOfficerDefinitionCode { get; set; }

        [DataMember]
        public string AccountOfficerDefinitionName { get; set; }

        [DataMember]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        public string AccountOfficerName { get; set; }

        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
