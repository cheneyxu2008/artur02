using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicSite.Models
{
    public class SelfEvaluationCriteria
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string L0 { get; set; }
        public string L1 { get; set; }
        public string L2 { get; set; }
        public string L3 { get; set; }
        public string Comment { get; set; }
    }
}