using Fintrak.Client.Core.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.SystemCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class UserMISModel
    {
        public UserMIS UserMIS { get; set; }
        public UserClassificationModel[] Classifications { get; set; }
    }
}