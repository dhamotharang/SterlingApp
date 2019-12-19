using System;
using System.Linq;
using Fintrak.Client.IFRS.Entities;

namespace Fintrak.Presentation.WebClient.Models
{
    public class GLMappingModel
    {
        public GLMapping GLMapping { get; set; }
        public int Status { get; set; }
    }
}