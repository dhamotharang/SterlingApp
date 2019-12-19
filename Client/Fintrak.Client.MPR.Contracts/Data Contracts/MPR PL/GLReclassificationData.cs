using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Framework;


namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class GLReclassificationData : DataContractBase
    {
        [DataMember]
        public int GLReclassificationId { get; set; }

        [DataMember]
        public string GLAccount { get; set; }

        [DataMember]
        public string CaptionCode { get; set; }

        [DataMember]
        public string CaptionName { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

       [DataMember]
        public bool Active { get; set; }
    }
}
