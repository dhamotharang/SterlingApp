using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Basic.Framework;


namespace Fintrak.Client.Basic.Contracts
{
    [DataContract]
    public class ExpenseProductMappingData : DataContractBase
    {
        [DataMember]
        public int ExpenseProductId { get; set; }

        [DataMember]
        public string BasisCode { get; set; }

        [DataMember]
        public string BasisName { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }  

       [DataMember]
        public bool Active { get; set; }
    }
}
