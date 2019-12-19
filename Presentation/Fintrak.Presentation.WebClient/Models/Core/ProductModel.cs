using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class ProductModel
    {
        public Product Product { get; set; }
        public ProductTypeMappingData[] ProductTypeMappings { get; set; }
    }
}