using System;
using System.Linq;
using Fintrak.Client.Budget.Entities;

namespace Fintrak.Presentation.WebClient.Models
{
    public class OperationModel
    {
        public Operation Operation { get; set; }
        public OperationReview[] OperationReview { get; set; }
    }
}