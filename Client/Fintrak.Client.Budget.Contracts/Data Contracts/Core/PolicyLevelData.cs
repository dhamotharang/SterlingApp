using System;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Budget.Framework;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Budget.Contracts
{
    [DataContract]
    public class PolicyLevelData : DataContractBase
    {
        [DataMember]
        public int PolicyLevelId { get; set; }

        [DataMember]
        public string PolicyCode { get; set; }

        [DataMember]
        public string PolicyName { get; set; }

        [DataMember]
        public string ModuleCode { get; set; }

        [DataMember]
        public string ModuleName { get; set; }
      
        [DataMember]
        public string DefinitionCode { get; set; }

        [DataMember]
        public string DefinitionName { get; set; }

        [DataMember]
        public CenterTypeEnum Center { get; set; }

        [DataMember]
        public string CenterName { get; set; }

        [DataMember]
        public string ReviewCode { get; set; }

        [DataMember]
        public string ReviewName { get; set; }

        [DataMember]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}
