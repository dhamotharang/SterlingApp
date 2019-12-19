﻿using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.SystemCore.Entities
{
    public partial class CompanyUser : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int CompanyUserId { get; set; }

        [DataMember]
        [Required]
        public int UserId { get; set; }

        [DataMember]
        [Required]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return CompanyUserId;
            }
        }
    }
}
