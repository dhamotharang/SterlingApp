using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class TeamDeepModel
    {
        public int TeamId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }
        public string ParentName { get; set; }
        public string DefinitionCode { get; set; }
        public IEnumerable<TeamDeepModel> Children { get; set; }
        public int Depth { get; set; }

        public string LongDescription 
        {
            get
            {
                return string.Format("{0} - {1}", Name,ParentName);
            }
        
        }
    }
}