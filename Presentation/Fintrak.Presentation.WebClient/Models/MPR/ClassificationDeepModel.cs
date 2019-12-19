using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class ClassificationDeepModel
    {
        public int ClassificationId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }
        public string ParentName { get; set; }
        public string TypeCode { get; set; }
        public int Level { get; set; }
        public IEnumerable<ClassificationDeepModel> Children { get; set; }
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