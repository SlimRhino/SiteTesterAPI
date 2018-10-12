using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteTester.Models
{
    public struct SiteStatus
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime LastAvailable { get; set; }
        //string User { get; set; }
    }


}
