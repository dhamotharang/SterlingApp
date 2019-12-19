//using Fintrak.Client.MPR.Contracts;
//using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class CSVModel
    {
        public string UploadId { get; set; }
        public string Content { get; set; }
        public string Separator { get; set; }
        public bool Header { get; set; }
        public bool Truncate { get; set; }

        public bool PostUploadAction { get; set; }
    }
}