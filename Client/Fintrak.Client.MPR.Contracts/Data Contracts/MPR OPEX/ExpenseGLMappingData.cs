using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Framework;


namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class ExpenseGLMappingData : DataContractBase
    {
        [DataMember]
        public int ExpenseGLId { get; set; }

        [DataMember]
        public string BasisCode { get; set; }

        [DataMember]
        public string BasisName { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public string GLName { get; set; }    

        [DataMember]
        public bool Active { get; set; }
    }
}
