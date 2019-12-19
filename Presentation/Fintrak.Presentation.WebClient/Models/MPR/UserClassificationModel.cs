using Fintrak.Client.MPR.Entities;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Entities;
using System;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class UserClassificationModel
    {
        public int UserClassificationMapId { get; set; }
        public string LoginID { get; set; }
        public string ClassificationCode { get; set; }
        public string ClassificationName { get; set; }
        public int Level { get; set; }
        public string ClassificationTypeCode { get; set; }
        public string ClassificationTypeName { get; set; }
        public TeamClassification[] Classifications { get; set; }
    }
}