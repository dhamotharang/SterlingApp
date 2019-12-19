using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Core.Contracts
{
    [DataContract]
    public class ProcessTriggerData : DataContractBase
    {
        [DataMember]
        public int ProcessTriggerId { get; set; }

        [DataMember]
        public int ProcessId { get; set; }

        [DataMember]
        public string ProcessTitle { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public PackageStatus Status { get; set; }

        [DataMember]
        public string StatusName { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

       [DataMember]
        public bool Active { get; set; }
    }
}
