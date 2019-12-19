using System;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Budget.Framework;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Budget.Contracts
{
    [DataContract]
    public class OfficerDetailData : DataContractBase
    {
        [DataMember]
        public int OfficerDetailId { get; set; }

        [DataMember]
        public string MisCode { get; set; }

        [DataMember]
        public string MisName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string StaffID { get; set; }
      
        [DataMember]
        public string StaffName { get; set; }
                 
        [DataMember]
        public string DefinitionCode { get; set; }

        [DataMember]
        public string DefinitionName { get; set; }

        [DataMember]
        public string Mobile { get; set; }

        [DataMember]
        public string Email { get; set; }

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
