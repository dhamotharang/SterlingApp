using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class ImpairmentOverride : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ImpairmentOverrideId { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        [Required]
        public string AccountNo { get; set; }

        [DataMember]
        [Required]
        public ImpairmentClassification Classification { get; set; }

        [DataMember]
        public string Reason { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return ImpairmentOverrideId;
            }
        }
    }
}
