using System;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Budget.Framework;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Budget.Contracts
{
    [DataContract]
    public class ModificationLevelData : DataContractBase
    {
        [DataMember]
        public int ModificationLevelId { get; set; }

        [DataMember]
        public string ModuleCode { get; set; }

        [DataMember]
        public string ModuleName { get; set; }
      
        [DataMember]
        public string DefinitionCode { get; set; }

        [DataMember]
        public string DefinitionName { get; set; }

        [DataMember]
        public bool Status { get; set; }
     
        [DataMember]
        public bool Active { get; set; }
    }
}
