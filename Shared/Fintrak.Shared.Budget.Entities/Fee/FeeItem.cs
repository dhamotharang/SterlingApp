using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class FeeItem : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int FeeItemId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public string GroupCode { get; set; }

        [DataMember]
        [Required]
        public string CaptionCode { get; set; }

        [DataMember]
        [Required]
        public string CategoryCode { get; set; }

        [DataMember]
        [Required]
        public string CalculationType { get; set; }

        [DataMember]
        [Required]
        public string Movement { get; set; }

        [DataMember]
        [Required]
        public FeeUnitEnum Unit { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }

        [DataMember]
        public bool Budgetable { get; set; }

        [DataMember]
        [Required]
        public string ReviewCode { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        public int EntityId
        {
            get
            {
                return FeeItemId;
            }
        }
    }
}
